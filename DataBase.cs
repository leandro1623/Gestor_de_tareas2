using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeTareas
{
    public sealed class DataBase
    {
        public string Path { get; private set; }
        
        public DataBase(string path)
        {
            this.Path = path;
        }

        ~DataBase()
        {
            //Destructor
        }

        /// <summary>
        /// Agrega texto al archivo
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool Add(string content)
        {
            System.IO.File.AppendAllText(Path,content+System.Environment.NewLine);
            return System.IO.File.Exists(Path) ? true : false;
        }

        public bool Add(string[] content)
        {
            System.IO.File.AppendAllLines(Path, content);
            //System.IO.File.AppendAllText(Path, System.Environment.NewLine);
            return System.IO.File.Exists(Path) ? true : false;
        }

        /// <summary>
        /// Borra el archivo
        /// </summary>
        public void DeleteFile()
        {
            System.IO.File.Delete(this.Path);
        }


        /// <summary>
        /// Elimina todo el texto del archivo
        /// </summary>
        public void DeleteAlltextInFile()
        {
            this.DeleteFile();
            System.IO.File.Create(this.Path);
        }

        /// <summary>
        /// Elimina una linea en espcifico
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void DeleteEspecificLine(int index)
        {
            string[] cont = System.IO.File.ReadAllLines(this.Path);
            string[] result=new string[cont.Length-1];

            for(int i = 0; i < cont.Length; i++)
            {
                if (i != index)
                {
                    result[i] = cont[i];
                }
            }
            this.DeleteFile();
            this.Add(result);
        }
    }
}
