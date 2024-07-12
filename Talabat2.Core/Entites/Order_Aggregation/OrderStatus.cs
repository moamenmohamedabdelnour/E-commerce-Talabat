using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat2.Core.Entites.Order_Aggregation
{
    public enum OrderStatus
    {
        //Will be Column In Order Table So Want To Save It As Words Not int 
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value ="Paymrnt Recived")]
        PaymentRecived,
        [EnumMember(Value ="Payment Failed")]
        PaymentFailed
    }
}
