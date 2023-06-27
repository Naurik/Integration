namespace GGKService.Common.Interfaces
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://bip.bee.kz/SyncChannel/v10/Types")]
    public partial class SendMessage {
        
        private SyncSendMessageRequest requestField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public SyncSendMessageRequest request {
            get {
                return this.requestField;
            }
            set {
                this.requestField = value;
            }
        }
    }
}