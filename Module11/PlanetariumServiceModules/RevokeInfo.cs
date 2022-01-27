namespace PlanetariumServiceModules
{
    public class RevokeInfo
    {
        private int orderId;
        private string clientEmail;
        private string infoMessage;
        public RevokeInfo() { }
        public RevokeInfo(int orderId, string clientEmail, string infoMessage) {
            this.OrderId = orderId;
            this.clientEmail = clientEmail;
            this.infoMessage = infoMessage;
        }
        public string ClientEmail { get => clientEmail; set => clientEmail = value; }
        public string InfoMessage { get => infoMessage; set => infoMessage = value; }
        public int OrderId { get => orderId; set => orderId = value; }
    }
}
