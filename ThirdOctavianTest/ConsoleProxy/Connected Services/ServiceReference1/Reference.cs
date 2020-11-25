﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторного создания кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/WcfService")]
    public partial class User : object
    {
        
        private int DepartamentIdField;
        
        private System.Nullable<int> IdField;
        
        private string LoginField;
        
        private string PasswordField;
        
        private int RoleIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int DepartamentId
        {
            get
            {
                return this.DepartamentIdField;
            }
            set
            {
                this.DepartamentIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Login
        {
            get
            {
                return this.LoginField;
            }
            set
            {
                this.LoginField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RoleId
        {
            get
            {
                return this.RoleIdField;
            }
            set
            {
                this.RoleIdField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProcedureDB", Namespace="http://schemas.datacontract.org/2004/07/WcfService")]
    public enum ProcedureDB : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserInsert = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RoleInsert = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DeptInsert = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserUpdate = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RoleUpdate = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DeptUpdate = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserDelete = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RoleDelete = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DeptDelete = 8,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Role", Namespace="http://schemas.datacontract.org/2004/07/WcfService")]
    public partial class Role : object
    {
        
        private int IdField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Dept", Namespace="http://schemas.datacontract.org/2004/07/WcfService")]
    public partial class Dept : object
    {
        
        private int IdField;
        
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IListOfUserService")]
    public interface IListOfUserService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IListOfUserService/SetUserDetails", ReplyAction="http://tempuri.org/IListOfUserService/SetUserDetailsResponse")]
        System.Threading.Tasks.Task SetUserDetailsAsync(ServiceReference1.User uDetails, ServiceReference1.ProcedureDB nameProcedure);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IListOfUserService/GetUsers", ReplyAction="http://tempuri.org/IListOfUserService/GetUsersResponse")]
        System.Threading.Tasks.Task<ServiceReference1.User[]> GetUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IListOfUserService/GetRoles", ReplyAction="http://tempuri.org/IListOfUserService/GetRolesResponse")]
        System.Threading.Tasks.Task<ServiceReference1.Role[]> GetRolesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IListOfUserService/GetDepts", ReplyAction="http://tempuri.org/IListOfUserService/GetDeptsResponse")]
        System.Threading.Tasks.Task<ServiceReference1.Dept[]> GetDeptsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface IListOfUserServiceChannel : ServiceReference1.IListOfUserService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class ListOfUserServiceClient : System.ServiceModel.ClientBase<ServiceReference1.IListOfUserService>, ServiceReference1.IListOfUserService
    {
        
        /// <summary>
        /// Реализуйте этот разделяемый метод для настройки конечной точки службы.
        /// </summary>
        /// <param name="serviceEndpoint">Настраиваемая конечная точка</param>
        /// <param name="clientCredentials">Учетные данные клиента.</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ListOfUserServiceClient() : 
                base(ListOfUserServiceClient.GetDefaultBinding(), ListOfUserServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.binding1_IListOfUserService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ListOfUserServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(ListOfUserServiceClient.GetBindingForEndpoint(endpointConfiguration), ListOfUserServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ListOfUserServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ListOfUserServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ListOfUserServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ListOfUserServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ListOfUserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task SetUserDetailsAsync(ServiceReference1.User uDetails, ServiceReference1.ProcedureDB nameProcedure)
        {
            return base.Channel.SetUserDetailsAsync(uDetails, nameProcedure);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.User[]> GetUsersAsync()
        {
            return base.Channel.GetUsersAsync();
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.Role[]> GetRolesAsync()
        {
            return base.Channel.GetRolesAsync();
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.Dept[]> GetDeptsAsync()
        {
            return base.Channel.GetDeptsAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.binding1_IListOfUserService))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.binding1_IListOfUserService))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:8080/Users");
            }
            throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return ListOfUserServiceClient.GetBindingForEndpoint(EndpointConfiguration.binding1_IListOfUserService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return ListOfUserServiceClient.GetEndpointAddress(EndpointConfiguration.binding1_IListOfUserService);
        }
        
        public enum EndpointConfiguration
        {
            
            binding1_IListOfUserService,
        }
    }
}
