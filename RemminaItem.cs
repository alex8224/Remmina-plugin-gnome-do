// 
//  RemminaItem.cs
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
using System.Diagnostics;
using Do.Universe;

namespace Remmina
{
	public class RemminaItem:Item
	{
		private String itemname;
		private String prefpath;

		public String ItemName {
			get {
				return itemname;
			}
		}

		public String PrefPath {
			get {
				return prefpath;
			}
		}

		public override string Name {
			get {
				return itemname + "- remmina";
			}
		}

		public override string Description {
			get {
				return "Remmina connection file";
			}
		}

		public override string Icon {
			get {
				return Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location),"remmina.png"); 
			}
		}

		public void Connect() {
			Process.Start ("/usr/bin/remmina", String.Format("-c {0}", this.PrefPath));
		}

		public static void NewConnection() {
			Process.Start ("/usr/bin/remmina", "-n");
		}

		public void DeleteConnection() {
			Process.Start("/bin/rm", this.PrefPath);
		}

		public RemminaItem(String itemname, String prefpath) {
			this.itemname = itemname;
			this.prefpath = prefpath;
		}

	}
}

