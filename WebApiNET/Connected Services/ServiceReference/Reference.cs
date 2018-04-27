﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiNET.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TransferData", Namespace="http://schemas.datacontract.org/2004/07/OutlookService.DTOs")]
    [System.SerializableAttribute()]
    public partial class TransferData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IOutlookService", CallbackContract=typeof(WebApiNET.ServiceReference.IOutlookServiceCallback))]
    public interface IOutlookService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IOutlookService/Connect")]
        void Connect(int id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IOutlookService/Connect")]
        System.Threading.Tasks.Task ConnectAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IOutlookService/Disconnect")]
        void Disconnect(int id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IOutlookService/Disconnect")]
        System.Threading.Tasks.Task DisconnectAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IOutlookService/Callback")]
        void Callback();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IOutlookService/Callback")]
        System.Threading.Tasks.Task CallbackAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/SendData", ReplyAction="http://tempuri.org/IOutlookService/SendDataResponse")]
        WebApiNET.ServiceReference.TransferData SendData(WebApiNET.ServiceReference.TransferData list);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/SendData", ReplyAction="http://tempuri.org/IOutlookService/SendDataResponse")]
        System.Threading.Tasks.Task<WebApiNET.ServiceReference.TransferData> SendDataAsync(WebApiNET.ServiceReference.TransferData list);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/GetAppointments", ReplyAction="http://tempuri.org/IOutlookService/GetAppointmentsResponse")]
        BLL.EntitesDTO.AppointmentDTO[] GetAppointments();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/GetAppointments", ReplyAction="http://tempuri.org/IOutlookService/GetAppointmentsResponse")]
        System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO[]> GetAppointmentsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/GetAppointmentsWithSql", ReplyAction="http://tempuri.org/IOutlookService/GetAppointmentsWithSqlResponse")]
        BLL.EntitesDTO.AppointmentDTO[] GetAppointmentsWithSql(int id, int itemsToSkip, int pageSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/GetAppointmentsWithSql", ReplyAction="http://tempuri.org/IOutlookService/GetAppointmentsWithSqlResponse")]
        System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO[]> GetAppointmentsWithSqlAsync(int id, int itemsToSkip, int pageSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/GetAppointmentById", ReplyAction="http://tempuri.org/IOutlookService/GetAppointmentByIdResponse")]
        BLL.EntitesDTO.AppointmentDTO GetAppointmentById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/GetAppointmentById", ReplyAction="http://tempuri.org/IOutlookService/GetAppointmentByIdResponse")]
        System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO> GetAppointmentByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/UpdateAppointment", ReplyAction="http://tempuri.org/IOutlookService/UpdateAppointmentResponse")]
        BLL.EntitesDTO.AppointmentDTO UpdateAppointment(BLL.EntitesDTO.AppointmentDTO appointment);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/UpdateAppointment", ReplyAction="http://tempuri.org/IOutlookService/UpdateAppointmentResponse")]
        System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO> UpdateAppointmentAsync(BLL.EntitesDTO.AppointmentDTO appointment);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/RemoveAppointmentById", ReplyAction="http://tempuri.org/IOutlookService/RemoveAppointmentByIdResponse")]
        BLL.EntitesDTO.AppointmentDTO RemoveAppointmentById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/RemoveAppointmentById", ReplyAction="http://tempuri.org/IOutlookService/RemoveAppointmentByIdResponse")]
        System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO> RemoveAppointmentByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/AddAppointment", ReplyAction="http://tempuri.org/IOutlookService/AddAppointmentResponse")]
        BLL.EntitesDTO.AppointmentDTO AddAppointment(BLL.EntitesDTO.AppointmentDTO appointment, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/AddAppointment", ReplyAction="http://tempuri.org/IOutlookService/AddAppointmentResponse")]
        System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO> AddAppointmentAsync(BLL.EntitesDTO.AppointmentDTO appointment, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/SendDataTo", ReplyAction="http://tempuri.org/IOutlookService/SendDataToResponse")]
        string SendDataTo(WebApiNET.ServiceReference.TransferData list, int recipientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/SendDataTo", ReplyAction="http://tempuri.org/IOutlookService/SendDataToResponse")]
        System.Threading.Tasks.Task<string> SendDataToAsync(WebApiNET.ServiceReference.TransferData list, int recipientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/GetCountAppointments", ReplyAction="http://tempuri.org/IOutlookService/GetCountAppointmentsResponse")]
        int GetCountAppointments();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/GetCountAppointments", ReplyAction="http://tempuri.org/IOutlookService/GetCountAppointmentsResponse")]
        System.Threading.Tasks.Task<int> GetCountAppointmentsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOutlookServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/CallbackEmpty", ReplyAction="http://tempuri.org/IOutlookService/CallbackEmptyResponse")]
        void CallbackEmpty();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOutlookService/CallbackFull", ReplyAction="http://tempuri.org/IOutlookService/CallbackFullResponse")]
        void CallbackFull(WebApiNET.ServiceReference.TransferData list);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOutlookServiceChannel : WebApiNET.ServiceReference.IOutlookService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OutlookServiceClient : System.ServiceModel.DuplexClientBase<WebApiNET.ServiceReference.IOutlookService>, WebApiNET.ServiceReference.IOutlookService {
        
        public OutlookServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public OutlookServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public OutlookServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public OutlookServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public OutlookServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Connect(int id) {
            base.Channel.Connect(id);
        }
        
        public System.Threading.Tasks.Task ConnectAsync(int id) {
            return base.Channel.ConnectAsync(id);
        }
        
        public void Disconnect(int id) {
            base.Channel.Disconnect(id);
        }
        
        public System.Threading.Tasks.Task DisconnectAsync(int id) {
            return base.Channel.DisconnectAsync(id);
        }
        
        public void Callback() {
            base.Channel.Callback();
        }
        
        public System.Threading.Tasks.Task CallbackAsync() {
            return base.Channel.CallbackAsync();
        }
        
        public WebApiNET.ServiceReference.TransferData SendData(WebApiNET.ServiceReference.TransferData list) {
            return base.Channel.SendData(list);
        }
        
        public System.Threading.Tasks.Task<WebApiNET.ServiceReference.TransferData> SendDataAsync(WebApiNET.ServiceReference.TransferData list) {
            return base.Channel.SendDataAsync(list);
        }
        
        public BLL.EntitesDTO.AppointmentDTO[] GetAppointments() {
            return base.Channel.GetAppointments();
        }
        
        public System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO[]> GetAppointmentsAsync() {
            return base.Channel.GetAppointmentsAsync();
        }
        
        public BLL.EntitesDTO.AppointmentDTO[] GetAppointmentsWithSql(int id, int itemsToSkip, int pageSize) {
            return base.Channel.GetAppointmentsWithSql(id, itemsToSkip, pageSize);
        }
        
        public System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO[]> GetAppointmentsWithSqlAsync(int id, int itemsToSkip, int pageSize) {
            return base.Channel.GetAppointmentsWithSqlAsync(id, itemsToSkip, pageSize);
        }
        
        public BLL.EntitesDTO.AppointmentDTO GetAppointmentById(int id) {
            return base.Channel.GetAppointmentById(id);
        }
        
        public System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO> GetAppointmentByIdAsync(int id) {
            return base.Channel.GetAppointmentByIdAsync(id);
        }
        
        public BLL.EntitesDTO.AppointmentDTO UpdateAppointment(BLL.EntitesDTO.AppointmentDTO appointment) {
            return base.Channel.UpdateAppointment(appointment);
        }
        
        public System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO> UpdateAppointmentAsync(BLL.EntitesDTO.AppointmentDTO appointment) {
            return base.Channel.UpdateAppointmentAsync(appointment);
        }
        
        public BLL.EntitesDTO.AppointmentDTO RemoveAppointmentById(int id) {
            return base.Channel.RemoveAppointmentById(id);
        }
        
        public System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO> RemoveAppointmentByIdAsync(int id) {
            return base.Channel.RemoveAppointmentByIdAsync(id);
        }
        
        public BLL.EntitesDTO.AppointmentDTO AddAppointment(BLL.EntitesDTO.AppointmentDTO appointment, int id) {
            return base.Channel.AddAppointment(appointment, id);
        }
        
        public System.Threading.Tasks.Task<BLL.EntitesDTO.AppointmentDTO> AddAppointmentAsync(BLL.EntitesDTO.AppointmentDTO appointment, int id) {
            return base.Channel.AddAppointmentAsync(appointment, id);
        }
        
        public string SendDataTo(WebApiNET.ServiceReference.TransferData list, int recipientId) {
            return base.Channel.SendDataTo(list, recipientId);
        }
        
        public System.Threading.Tasks.Task<string> SendDataToAsync(WebApiNET.ServiceReference.TransferData list, int recipientId) {
            return base.Channel.SendDataToAsync(list, recipientId);
        }
        
        public int GetCountAppointments() {
            return base.Channel.GetCountAppointments();
        }
        
        public System.Threading.Tasks.Task<int> GetCountAppointmentsAsync() {
            return base.Channel.GetCountAppointmentsAsync();
        }
    }
}
