using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestorDeTareas
{
    public partial class Gestor_de_tareas : Form
    { 
        private bool IsNewTask;
        private bool CambioTarea;
        private DataBase DataBase { get; set; }
        System.Collections.Generic.List<string> Tasks = new List<string>();


        public Gestor_de_tareas()//fMain
        {
            InitializeComponent();
            this.DataBase = new DataBase("Tasks.txt");
            _Reset();
        }

        private void _Reset()
        {
            this.ShowingTasks();
            //Controls 
            this.AddNewTask.Enabled = true;
            this.SaveChanges.Enabled = false;
            this.Delete.Enabled = false;
            this.Cancel.Enabled = false;
            this.TexBox.Enabled = false;
            this.TexBox.Text = "";
            this.CambioTarea = false;


            //ListTask eneabled bassed on numbers counts 
            this.ListTask.Enabled = this.ListTask.Items.Count > 0? true : false;
            this.ListTask.SelectedIndex = -1;
        }

        private void ShowingTasks()
        {
            //showing Tasks
            if (System.IO.File.Exists(this.DataBase.Path))
            {
                Tasks.Clear();
                Tasks.AddRange(System.IO.File.ReadAllLines(this.DataBase.Path));
                ListTask.Items.Clear();
                ListTask.Items.AddRange(Tasks.ToArray());
            }            
        }

        private void AgregarNuevaTarea()
        {
            this.SaveChanges.Enabled = true;
            this.AddNewTask.Enabled = false;
            this.TexBox.Enabled = true;
            this.Cancel.Enabled = true;
           
            this.TexBox.Focus();
            this.IsNewTask = true;
        }

        private void Guardar()
        {
            if (this.TexBox.Text.Length > 0)
            {
                if (this.IsNewTask)
                {
                    this.DataBase.Add(this.TexBox.Text);
                    this.ListTask.Items.Add(this.TexBox.Text);
                    this._Reset();
                }
                else
                {
                    this.ListTask.Items[this.ListTask.SelectedIndex] = this.TexBox.Text;
                    _Reset();
                }
            }
            else
            {
                MessageBox.Show("Debe agregar una tarea!.", "ADEVERTENCIA!", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Eliminar()
        {
            int index = this.ListTask.SelectedIndex;
            if (MessageBox.Show("Estas seguro de eliminar esta tarea?", "Confirmar eliminacion", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.ListTask.Items.RemoveAt(index);
                this.DataBase.DeleteEspecificLine(index);
                this._Reset();
            }
        }

        private void Cancelar()
        {
            if (CambioTarea == true && MessageBox.Show("Desea cancelar esta accion?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _Reset();
            }
            else if(CambioTarea==false)
            {
                _Reset();

            }
        }

        private void TareaSeleccionada()
        {
            if (this.ListTask.SelectedIndex >= 0 && this.ListTask.SelectedIndex < this.ListTask.Items.Count)
            {
                this.TexBox.Text = this.ListTask.Items[this.ListTask.SelectedIndex].ToString();
                this.Delete.Enabled = true;
                this.SaveChanges.Enabled = true;
                this.AddNewTask.Enabled = false;
                this.TexBox.Enabled = true;
                this.Cancel.Enabled = true;
                this.IsNewTask = false;
            }            
        }
        
        //----------------------


        private void AddNewTask_Click(object sender, EventArgs e)
        {
            this.AgregarNuevaTarea();
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            this.Guardar();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            this.Eliminar();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Cancelar();
        }

        private void TexBox_TextChanged(object sender, EventArgs e)
        {
            CambioTarea = true;
        }

        private void ListTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TareaSeleccionada();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Gestor_de_tareas_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = MessageBox.Show("Desea guardar cambios", "Guardar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (this.CambioTarea==true && DialogResult == DialogResult.Yes)
            {
                this.Guardar();
            }
            else if(DialogResult == DialogResult.Cancel)
            {
                e.Cancel = true;
            }

        }
    }
}
 