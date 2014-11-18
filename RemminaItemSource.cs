// 
//  RemminaItemSource.cs
//  
//  Author:
//       Alex Zhang <alex8224@gmail.com>
// 
//  Copyright © 2014 Alex Zhang <alex8224@gmail.com>
// 
//  This library is free software; you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 2.1 of the
//  License, or (at your option) any later version.
// 
//  This library is distributed in the hope that it will be useful, but
//  WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Do.Universe;
namespace Remmina
{

	public class RemminaItemSource: DynamicItemSource
	{
		private Dictionary<String, RemminaItem> remminaItemList = new Dictionary<string, RemminaItem>();

		public override IEnumerable<Type> SupportedItemTypes {
			get {
				yield return typeof(RemminaItem);
			}
		}

		private static String ParseRemminaItem(String prefpath) {
			StreamReader reader = null;
			String line = "";
			try{
				reader = File.OpenText(prefpath);
				for (;;) {
					line = reader.ReadLine ();
					if (line == null)
						break;
					if (line != null) {
						if (line.StartsWith ("name=")) {
							return line.Split (new char[]{'='}, 2)[1];
						}
					}
				}
				return line;
			}catch(Exception){
				return line;
			}finally{
				if (reader != null)
					reader.Close ();
			}
		}

		protected override void Enable ()
		{
			ItemsAvailableEventArgs eventArgs = new ItemsAvailableEventArgs ();

			lock(remminaItemList) {
				String remminaconfdir = Path.Combine (Environment.GetEnvironmentVariable ("HOME"), ".remmina");
				FileSystemWatcher watcher = new FileSystemWatcher (remminaconfdir, "*.*");
				watcher.Renamed += new RenamedEventHandler (FileCreated);
				watcher.Deleted += new FileSystemEventHandler (FileDeleted);
				watcher.EnableRaisingEvents = true;
				IEnumerable<String> allfiles = Directory.GetFiles (remminaconfdir).Where (file => file.EndsWith (".remmina"));
				foreach (String remminaprefpath in allfiles) {
					Console.WriteLine (remminaprefpath);
					String itemname = ParseRemminaItem (remminaprefpath);
					if (itemname == null)
						return;
					remminaItemList [itemname] = new RemminaItem(itemname, remminaprefpath);

				}
				eventArgs.newItems = remminaItemList.Values;
			}

			RaiseItemsAvailable (eventArgs);
		}

		private void FileCreated(object sender, RenamedEventArgs args) {

			Console.WriteLine ("add new item {0}", args.FullPath);
			if(!Connected) return;
			String remminapref = args.FullPath;
			String itemname = ParseRemminaItem (remminapref);
			if (remminaItemList.ContainsKey (itemname))
				return;
			RemminaItem item = new RemminaItem (itemname, remminapref);
			RaiseItemsAvailable (new ItemsAvailableEventArgs () { newItems = new Item[]{item}});
		
		}

		private void FileDeleted(object sender, FileSystemEventArgs args) {
			Console.WriteLine(args.FullPath + "----deleted!");
			if(!Connected) return;
			String remminapref = args.FullPath;
			String itemname = remminaItemList.Where (pair => pair.Value.PrefPath == remminapref).First ().Key;
		
			if (remminaItemList.ContainsKey (itemname)) {
				RemminaItem item = remminaItemList[itemname];
				remminaItemList.Remove (itemname);
				RaiseItemsUnavailable (new ItemsUnavailableEventArgs () { unavailableItems = new Item[]{item}});
			}
		}

		protected override void Disable ()
		{
			lock (remminaItemList) {
				remminaItemList.Clear ();
			}
		}

		public override string Name {
			get {
				return "Remmina Source";
			}
		}

		public override string Description {
			get {
				return "Remmina Source";
			}
		}

		public override string Icon {
			get {
				return Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location),"remmina.png"); 
			}
		}
	}
}

