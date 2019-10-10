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