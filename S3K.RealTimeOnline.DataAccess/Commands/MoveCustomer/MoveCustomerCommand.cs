﻿using S3K.RealTimeOnline.Domain;

namespace S3K.RealTimeOnline.DataAccess.Commands.MoveCustomer
{
    public class MoveCustomerCommand
    {
        public int CustomerId { get; set; }

        public Address NewAddress { get; set; }
    }
}