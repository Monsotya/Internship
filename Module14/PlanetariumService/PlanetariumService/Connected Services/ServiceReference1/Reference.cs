//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IPlanetariumService")]
    public interface IPlanetariumService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlanetariumService/BuyTickets", ReplyAction="http://tempuri.org/IPlanetariumService/BuyTicketsResponse")]
        System.Threading.Tasks.Task<bool> BuyTicketsAsync(int ticketId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlanetariumService/SendEmail", ReplyAction="http://tempuri.org/IPlanetariumService/SendEmailResponse")]
        System.Threading.Tasks.Task<string> SendEmailAsync(string name, string seats);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface IPlanetariumServiceChannel : ServiceReference1.IPlanetariumService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class PlanetariumServiceClient : System.ServiceModel.ClientBase<ServiceReference1.IPlanetariumService>, ServiceReference1.IPlanetariumService
    {
        
        public PlanetariumServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<bool> BuyTicketsAsync(int ticketId)
        {
            return base.Channel.BuyTicketsAsync(ticketId);
        }
        
        public System.Threading.Tasks.Task<string> SendEmailAsync(string name, string seats)
        {
            return base.Channel.SendEmailAsync(name, seats);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
    }
}
