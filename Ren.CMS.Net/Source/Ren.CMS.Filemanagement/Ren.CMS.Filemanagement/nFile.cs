using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Filemanagement
{
    public class nFile
    {
        public int Id { get; set; }
 
        public int FileReference { get;set;}

        public string AliasName { get; set; }

        public string FilePath { get; set; }

        public bool isActive { get; set; }

        public bool Physical { get;set;}

        public long FileSize { get; set; }

        public List<nFile> ReferencedFiles { get; set; }

        public nFile()
        {
            this.ReferencedFiles = new List<nFile>();
        }

        public nFile(string aliasName, string filePath, bool isactive, bool physical, int FileReference = 0)
        {

            this.AliasName = aliasName;
            this.FilePath = filePath;
            this.isActive = isactive;
            this.Physical = physical;
            this.FileReference = FileReference;
            this.ReferencedFiles = new List<nFile>();
        }

        public nFile(Ren.CMS.Persistence.Domain.File pFile)
        {
            this.Id = pFile.Id;
            this.AliasName = pFile.AliasName;
            this.FilePath = pFile.FilePath;
            this.isActive = pFile.isActive;
            this.Physical = pFile.Physical;
            this.ReferencedFiles = new List<nFile>();
            this.FileSize = pFile.FileSize;


            if (pFile.ReferencedFiles != null && pFile.ReferencedFiles.Count > 0)
            {
                this.ReferencedFiles = new List<nFile>();
                foreach(Ren.CMS.Persistence.Domain.File sFile in pFile.ReferencedFiles)
                {
                    this.ReferencedFiles.Add(new nFile(sFile));
                }
            }
           
        }


        public void AddReferencedFile(nFile file)
        {
            if(this.FileReference > 0)
            {
                throw new Exception("File id#" + this.Id + " is allready referenced to file id#" + this.FileReference + " and cannot be referenced");
            }

            if (this.ReferencedFiles == null)
                file.ReferencedFiles = new List<nFile>();

            if (this.Id > 0)
            {
                file.FileReference = this.Id;
            }


            this.ReferencedFiles.Add(file);


        }


    
    }
}
