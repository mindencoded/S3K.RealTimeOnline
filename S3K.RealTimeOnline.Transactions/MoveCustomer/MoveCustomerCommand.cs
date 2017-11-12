﻿using S3K.RealTimeOnline.GenericDomain;

namespace S3K.RealTimeOnline.Transactions.MoveCustomer
{
    public class MoveCustomerCommand
    {
        public int CustomerId { get; set; }

        public Address NewAddress { get; set; }
    }
}