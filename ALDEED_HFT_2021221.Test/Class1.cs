using NUnit.Framework;
using System;

namespace ALDEED_HFT_2021221.Test
{
    public class Class1
    {
        private Mock<ICustomersRepository> customerRepo;
        private Mock<IOrdersRepository> orderRepo;
        private Mock<IServicesRepository> serviceRepo;
        private Mock<IConnectorRepository> connectorRepo;
        private Mock<IClownsRepository> clownRepo;
        private List<OrderCostsResults> expectedCostResults;
        private List<AgeGapResults> expectedAgeResults;
        private List<ClownSalaryResults> expectedClownEarningResults;
        private CompanyLogic companyLogic;

        /// <summary>
        /// This is the test method for my first non-crud method.
        /// </summary>
        [Test]
        public void TestOrderCosts()
        {
            var actualOrderCosts = this.companyLogic.OrderCosts();

            Assert.That(actualOrderCosts, Is.EquivalentTo(this.expectedCostResults));

            this.customerRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.orderRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.serviceRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.connectorRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.clownRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// This is the test method for my second non-crud method.
        /// </summary>
        [Test]
        public void TestClownEarnings()
        {
        }

        /// <summary>
        /// This is the test method my third non-crud method.
        /// </summary>
        [Test]
        public void TestRecommendedAgeGaps()
        {
        }

        /// <summary>
        /// This is something.
        /// </summary>
        [OneTimeSetUp]
        public void CreateLogicWithMocks()
        {

        }

        [Test]
        public void TestThis()
        {

        }
    }
}
