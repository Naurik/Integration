using System;
using DevExpress.Xpo;

namespace GGKService.Common.Objects.Settings{

    /// <summary>
    /// Конфигурационные значения
    /// </summary>
    [Serializable]
	public class ConfigParameter : XPCustomObject{

        public ConfigParameter(){
			CreatedDate = DateTime.Now;
			ModificationDate = DateTime.Now;
        }

		public ConfigParameter(Session session)
			: base(session){
			CreatedDate = DateTime.Now;
			ModificationDate = DateTime.Now;
        }

		/// <summary>
		/// Ключ
		/// </summary>
		public String Key { get; set; }

		/// <summary>
		/// Значение 
		/// </summary>
		[Size(2000)]
		public String Value { get; set; }

		/// <summary>
		/// Описание 
		/// </summary>
		[Size(2000)]
		public String Description { get; set; }

		public DateTime? CreatedDate { get; set; }

		public DateTime? ModificationDate { get; set; }
    }

}