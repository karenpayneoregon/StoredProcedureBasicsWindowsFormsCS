//INSTANT C# NOTE: Formerly VB project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace DataOperations
{
	namespace TypeClasses
	{
		public class ContactTypes
		{
			public int Identifier {get; set;}
			public string ContactType {get; set;}

			public override string ToString()
			{
				return ContactType;
			}
		}
	}
}