using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dupes.Core
{
    public class DupeAction
    {
        private FileSystemWatcher _sourceWatcher;
        private Pattern _pattern;

        /// <summary>
        /// Create a new Duplication with a set pattern.
        /// </summary>
        /// <param name="pattern">Pattern to follow for the duplication.</param>
        public DupeAction(Pattern pattern)
        {
            _pattern = pattern;
        }

        /// <summary>
        /// Start the duplication of the folder according to the pattern.
        /// </summary>
        public void Begin()
        {
            //copy the content to the destination.
            IEnumerable<string> files = Directory.EnumerateFiles(_pattern.Source);
            
            foreach (string item in files)
            {
                File.Copy(item, _pattern.Destination + "\\" + System.IO.Path.GetFileName(item));
            }
            //setup a watcher to keep in sync.
            _sourceWatcher = new FileSystemWatcher(_pattern.Source);
            _sourceWatcher.Changed += sourceWatcher_Changed;
            _sourceWatcher.Deleted += sourceWatcher_Deleted;
            _sourceWatcher.Created += sourceWatcher_Created;
            //TODO would be nice to include subs.
            _sourceWatcher.IncludeSubdirectories = false;
        }


        /// <summary>
        /// Stop the duplication of the folder.
        /// </summary>
        public void End()
        {
            _sourceWatcher.Dispose();
        }

        void sourceWatcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (File.Exists(e.FullPath))
                {
                    File.Copy(e.FullPath, _pattern.Destination + "\\" + e.Name);
                }
                else if (Directory.Exists(e.FullPath))
                {
                    //not dealing with directories yet.
                }
                else
                {
                    //what the hell is this path??
                }

            }
            catch (Exception)
            {
                //Pokemon catch.
                //TODO refine this and handle.
            }
        }

        void sourceWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                File.Delete(_pattern.Destination + "\\" + e.Name);
            }
            catch (Exception)
            {
                //Pokemon catch.
                //TODO refine this and handle.
            }
        }

        private void sourceWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (File.Exists(e.FullPath))
                {
                    File.Copy(e.FullPath, _pattern.Destination + "\\" + e.Name);
                }
                else if (Directory.Exists(e.FullPath))
                {
                    //not dealing with directories yet.
                }
                else
                {
                    //what the hell is this path??
                }
            }
            catch (Exception)
            {
                //Pokemon catch.
                //TODO refine this and handle.
            }
        }
    }
}
