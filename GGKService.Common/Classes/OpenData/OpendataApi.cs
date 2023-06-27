﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:2.0.50727.8000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace GGKService.Common.Classes.OpenData
{
    using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://bee.kz/egov/opendata/api/v1/types/request")]
    [System.Xml.Serialization.XmlRootAttribute("OpendataApiRequest", Namespace = "http://bee.kz/egov/opendata/api/v1/types/request", IsNullable = false)]
    public partial class OpendataApiRequest
    {

        private requestClientType clientField;

        private requestMetaType metaField;

        private requestRowType[] dataField;

        /// <remarks/>
        public requestClientType client
        {
            get
            {
                return this.clientField;
            }
            set
            {
                this.clientField = value;
            }
        }

        /// <remarks/>
        public requestMetaType meta
        {
            get
            {
                return this.metaField;
            }
            set
            {
                this.metaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("row", IsNullable = false)]
        public requestRowType[] data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://bee.kz/egov/opendata/api/v1/types/request")]
    public partial class requestClientType
    {

        private string loginField;

        private string passwordField;

        /// <remarks/>
        public string login
        {
            get
            {
                return this.loginField;
            }
            set
            {
                this.loginField = value;
            }
        }

        /// <remarks/>
        public string password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://bee.kz/egov/opendata/api/v1/types/request")]
    public partial class requestMetaType
    {

        private string indexField;

        private string versionField;

        /// <remarks/>
        public string index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        /// <remarks/>
        public string version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://bee.kz/egov/opendata/api/v1/types/request")]
    public partial class requestRowType
    {

        private string yearField;

        private string quarterField;

        private string kbk_codeField;

        private string knp_codeField;

        private string name_ruField;

        private string name_kkField;

        private string pay_countField;

        private decimal pay_sumField;

        /// <remarks/>
        public string year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }

        /// <remarks/>
        public string quarter
        {
            get
            {
                return this.quarterField;
            }
            set
            {
                this.quarterField = value;
            }
        }

        /// <remarks/>
        public string kbk_code
        {
            get
            {
                return this.kbk_codeField;
            }
            set
            {
                this.kbk_codeField = value;
            }
        }

        /// <remarks/>
        public string knp_code
        {
            get
            {
                return this.knp_codeField;
            }
            set
            {
                this.knp_codeField = value;
            }
        }

        /// <remarks/>
        public string name_ru
        {
            get
            {
                return this.name_ruField;
            }
            set
            {
                this.name_ruField = value;
            }
        }

        /// <remarks/>
        public string name_kk
        {
            get
            {
                return this.name_kkField;
            }
            set
            {
                this.name_kkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string pay_count
        {
            get
            {
                return this.pay_countField;
            }
            set
            {
                this.pay_countField = value;
            }
        }

        /// <remarks/>
        public decimal pay_sum
        {
            get
            {
                return this.pay_sumField;
            }
            set
            {
                this.pay_sumField = Decimal.Round(value,2);
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://bee.kz/egov/opendata/api/v1/types/response")]
    [System.Xml.Serialization.XmlRootAttribute("data", Namespace = "http://bee.kz/egov/opendata/api/v1/types/response", IsNullable = false)]
    public partial class OpendataApiResponse
    {
        private StatusType statusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StatusType status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("status", Namespace = "", IsNullable = false)]
    public partial class StatusType
    {

        private string codeField;

        private string messageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
}