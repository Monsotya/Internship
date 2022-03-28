using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PlanetariumServiceWCF
{
    [ServiceContract]
    public interface IPlanetariumService
    {
        [OperationContract]
        bool BuyTickets(int ticketId);
        [OperationContract]
        string SendEmail(string name, string seats);
    }
}
