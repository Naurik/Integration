namespace GGKService.Common.Interfaces
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://bip.bee.kz/SyncChannel/v10/types/Request")]
    public partial class SyncSendMessageRequest {
        
        private SyncMessageInfo requestInfoField;
        
        private MessageData requestDataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public SyncMessageInfo requestInfo {
            get {
                return this.requestInfoField;
            }
            set {
                this.requestInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public MessageData requestData {
            get {
                return this.requestDataField;
            }
            set {
                this.requestDataField = value;
            }
        }
    }
}