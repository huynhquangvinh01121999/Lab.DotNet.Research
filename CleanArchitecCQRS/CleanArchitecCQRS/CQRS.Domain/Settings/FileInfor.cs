using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EsuhaiHRM.Domain.Settings
{
    public class FileInfor
    {
        public FileInfor() { }
        public string title { get; set; }
        public string key { get; set; }
        public bool expanded { get; set; }
        public List<FileInfor> children;
        public bool isFolder { get; set; }
        public bool isLeaf { get; set; }
        public string fileUrl { get; set; }
        public string fileType { get; set; }
        public string icon { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        //public FileInfor(string strTitle
        //               , List<FileInfor> lstChildren
        //               , string strKey
        //               , bool isFolder
        //               , bool expanded
        //               , string fileType
        //               , string fileUrl
        //               , bool isLeaf
        //               , string icon)
        //{
        //    this.title = strTitle;
        //    this.children = lstChildren;
        //    this.key = strKey;
        //    this.isFolder = isFolder;
        //    this.expanded = expanded;
        //    this.fileType = fileType;
        //    this.fileUrl = fileUrl;
        //    this.isLeaf = isLeaf;
        //    this.icon = icon;
        //}
    }
}
