using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace BaoHiemNhanTho
{
    public class Customer
    {
        [BsonId]
        public ObjectId id { get; set; }

        public string customerId { get; set; }

        public Name name { get; set; }

        public DateTime dob { get; set; }

        public string gender { get; set; }

        public Address address { get; set; }

        public string phoneNumber { get; set; }

        public string email { get; set; }

        public List<InsurancePolicy> insurancePolicies { get; set; }

        public List<Payment> payments { get; set; }
    }

    public class Name
    {
        public string firstName { get; set; }

        public string lastName { get; set; }
    }

    public class Address
    {
        public string street { get; set; }

        public string city { get; set; }

        public string country { get; set; }
    }

    public class InsurancePolicy
    {
        public ObjectId policyId { get; set; }

        public string policyNumber { get; set; }

        public string policyType { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public decimal premium { get; set; }

        public decimal coverageAmount { get; set; }

        public List<Beneficiary> beneficiaries { get; set; }

        public List<Claim> claims { get; set; }
    }

    public class Beneficiary
    {
        public string name { get; set; }

        public string relationship { get; set; }

        public int percentage { get; set; }
    }

    public class Claim
    {
        public ObjectId claimId { get; set; }

        public string claimNumber { get; set; }

        public DateTime dateOfClaim { get; set; }

        public decimal claimAmount { get; set; }

        public string status { get; set; }

        public string description { get; set; }
    }

    public class Payment
    {
        public ObjectId paymentId { get; set; }

        public decimal amount { get; set; }

        public DateTime paymentDate { get; set; }

        public string paymentMethod { get; set; }
    }
}
