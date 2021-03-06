﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;

namespace CodeDoc.Model
{
    public class CDFolder : CDTreeViewItem
    {

        // Fields
        #region Fields


        private string _path = Environment.GetEnvironmentVariable("LocalAppData") + "/Autodesk/3dsMax/2016 - 64bit/ENU/scripts/SugzTools/";


        #endregion // End Fields



        // Constructors
        #region Constructor


        public CDFolder(string text) : this(text, text) { }
        

        public CDFolder(string text, string path)
        {
            Type = ItemType.Folder;
            Text = text;
            _path += path;
        }


        #endregion // End Constructors



        // Methods
        #region Methods

        /// <summary>
        /// Folder have an ampty description
        /// </summary>
        protected override StringCollection GetDescription()
        {
            return null;
        }

        /// <summary>
        /// Get all the script in the folder
        /// </summary>
        protected override ObservableCollection<ICDTreeViewItem> GetChildren()
        {
            ObservableCollection<ICDTreeViewItem> children = new ObservableCollection<ICDTreeViewItem>();
            if (Directory.Exists(_path))
            {
                foreach (string file in Directory.GetFiles(_path))
                {
                    StreamReader stream = new StreamReader(file);
                    stream.ReadLine(); // skip the first line
                    string title = stream.ReadLine();

                    // Remove "Sugztools"
                    int index = title.IndexOf("SugzTools ");
                    title = (index < 0) ? title : title.Remove(index, ("SugzTools ").Length);

                    // Remove "Library"
                    index = title.IndexOf(" Library");
                    title = (index < 0) ? title : title.Remove(index, (" Library").Length);

                    children.Add(new CDScript(title, file));
                }
            }

            return children;
        }


        #endregion // End Methods

    }
}
