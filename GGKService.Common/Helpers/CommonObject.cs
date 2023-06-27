using System;

namespace GGKService.Common.Helpers {

	public abstract class CommonObject {
		public long Oid { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModificationDate { get; set; }
	}
}
