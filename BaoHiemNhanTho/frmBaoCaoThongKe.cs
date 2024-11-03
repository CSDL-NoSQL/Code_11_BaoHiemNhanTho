using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace BaoHiemNhanTho
{
    public partial class frmBaoCaoThongKe : Form
    {
        private IMongoCollection<Customer> _customerCollection;
        public frmBaoCaoThongKe()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("BHNT");
            _customerCollection = database.GetCollection<Customer>("BaoHiemNhanTho");

        }
        private void GenerateStatistics()
        {
            // Tổng số khách hàng
            var totalCustomers = _customerCollection.CountDocuments(new BsonDocument());

            // Tổng số hợp đồng bảo hiểm
            var result = _customerCollection.Aggregate()
                .Unwind("insurancePolicies")
                .Group(new BsonDocument { { "_id", BsonNull.Value }, { "count", new BsonDocument("$sum", 1) } })
                .FirstOrDefault();

            int totalPolicies = result != null && result.Contains("count")
                ? result["count"].ToInt32()
                : 0;

            // Tổng số phí bảo hiểm đã thanh toán
            var totalPremiumPaid = _customerCollection.Aggregate()
                .Unwind("payments")
                .Group(new BsonDocument { { "_id", BsonNull.Value }, { "totalAmount", new BsonDocument("$sum", "$payments.amount") } })
                .FirstOrDefault()
                ?.GetValue("totalAmount", 0)
                .ToInt32() ?? 0;

            // Hiển thị lên các label trong Form
            txtTotalCustomers.Text = $"Tổng số khách hàng: {totalCustomers}";
            txtTotalPolicies.Text = $"Tổng số hợp đồng bảo hiểm: {totalPolicies}";
            txtTotalPremiumPaid.Text = $"Tổng số phí bảo hiểm đã thanh toán: {totalPremiumPaid} VND";
        }

        

        
        private void GenerateTimeBasedStatistics(DateTime startDate, DateTime endDate)
        {
            // Tổng số hợp đồng bảo hiểm trong khoảng thời gian
            var result = _customerCollection.Aggregate()
             .Unwind("insurancePolicies")
             .Match(new BsonDocument
             {
                { "$expr", new BsonDocument
                    {
                        { "$and", new BsonArray
                            {
                                new BsonDocument
                                {
                                    { "$gte", new BsonArray
                                        {
                                            new BsonDocument("$dateFromString", new BsonDocument
                                                {
                                                    { "dateString", "$insurancePolicies.startDate" }
                                                }),
                                            startDate
                                        }
                                    }
                                },
                                new BsonDocument
                                {
                                    { "$lte", new BsonArray
                                        {
                                            new BsonDocument("$dateFromString", new BsonDocument
                                                {
                                                    { "dateString", "$insurancePolicies.endDate" }
                                                }),
                                            endDate
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
             })
             .Group(new BsonDocument
             {
                { "_id", BsonNull.Value }, // Nhóm tất cả lại với nhau
                { "count", new BsonDocument("$sum", 1) } // Tính tổng số lượng
             })
             .FirstOrDefault();

            // Lấy tổng số chính sách
            int policyCount = result != null && result.Contains("count")
                ? result["count"].ToInt32()
                : 0;


            // Thống kê tổng số yêu cầu bồi thường trong khoảng thời gian
            var result_2 = _customerCollection.Aggregate()
            .Unwind("insurancePolicies")
            .Unwind("insurancePolicies.claims")
            .Match(new BsonDocument
            {
                { "$expr", new BsonDocument
                    {
                        { "$and", new BsonArray
                            {
                                new BsonDocument
                                {
                                    { "$gte", new BsonArray
                                        {
                                            new BsonDocument("$dateFromString", new BsonDocument
                                                {
                                                    { "dateString", "$insurancePolicies.claims.dateOfClaim" }
                                                }),
                                            startDate
                                        }
                                    }
                                },
                                new BsonDocument
                                {
                                    { "$lte", new BsonArray
                                        {
                                            new BsonDocument("$dateFromString", new BsonDocument
                                                {
                                                    { "dateString", "$insurancePolicies.claims.dateOfClaim" }
                                                }),
                                            endDate
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            })
            .Group(new BsonDocument
            {
                { "_id", BsonNull.Value }, // Nhóm tất cả lại với nhau
                { "count", new BsonDocument("$sum", 1) } // Tính tổng số lượng
            })
            .FirstOrDefault();

            // Lấy tổng số yêu cầu
            int totalClaims = result_2 != null && result_2.Contains("count")
                ? result_2["count"].ToInt32()
                : 0;

            // Thống kê tổng phí bảo hiểm đã thanh toán trong khoảng thời gian
            var result_3 = _customerCollection.Aggregate()
            .Unwind("payments")
            .Match(new BsonDocument
            {
                { "$expr", new BsonDocument
                    {
                        { "$and", new BsonArray
                            {
                                new BsonDocument
                                {
                                    { "$gte", new BsonArray
                                        {
                                            new BsonDocument("$dateFromString", new BsonDocument
                                                {
                                                    { "dateString", "$payments.paymentDate" }
                                                }),
                                            startDate
                                        }
                                    }
                                },
                                new BsonDocument
                                {
                                    { "$lte", new BsonArray
                                        {
                                            new BsonDocument("$dateFromString", new BsonDocument
                                                {
                                                    { "dateString", "$payments.paymentDate" }
                                                }),
                                            endDate
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            })
            .Group(new BsonDocument
            {
                { "_id", BsonNull.Value }, // Nhóm tất cả lại với nhau
                { "totalAmount", new BsonDocument("$sum", "$payments.amount") }
            })
            .FirstOrDefault();

            // Lấy tổng số tiền
            int totalPremiumPaid = result_3 != null && result_3.Contains("totalAmount")
                ? result_3["totalAmount"].ToInt32()
                : 0;

            // Hiển thị kết quả thống kê
            txtPolicyCount.Text = $"Số hợp đồng: {policyCount}";
            txtTotalClaims.Text = $"Số yêu cầu bồi thường: {totalClaims}";
            txtTotalPremium.Text = $"Tổng phí bảo hiểm đã thanh toán: {totalPremiumPaid} VND";
        }

        private void frmBaoCaoThongKe_Load_1(object sender, EventArgs e)
        {
            GenerateStatistics();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DateTime startDate = startDatePicker.Value.Date;
            DateTime endDate = endDatePicker.Value.Date.AddDays(1).AddTicks(-1); // Đến cuối ngày của ngày kết thúc

            GenerateTimeBasedStatistics(startDate, endDate);
        }
    }
}
