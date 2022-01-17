namespace vivdly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into MembershipTypes(Id, SignupFee, durationMonth, Discount) values(1, 0, 0, 0)");
            Sql("Insert into MembershipTypes(Id, SignupFee, durationMonth, Discount) values(2, 30, 1, 10)");
            Sql("Insert into MembershipTypes(Id, SignupFee, durationMonth, Discount) values(3, 90, 3, 15)");
            Sql("Insert into MembershipTypes(Id, SignupFee, durationMonth, Discount) values(4, 300, 12, 20)");
        }
        
        public override void Down()
        {
        }
    }
}
