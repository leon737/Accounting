﻿using System;

namespace Cash.Web.Areas.Cash.Models
{
    public class AccountTransactionRelatedToAccountViewModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public decimal PreBalance { get; set; }

        public decimal PostBalance { get; set; }

        public Guid CorrespondingAccountId { get; set; }

        public BaseAccountRelationFrom RelationFrom { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string Remark { get; set; }

    }
}