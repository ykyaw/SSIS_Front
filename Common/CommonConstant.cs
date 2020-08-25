using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Common
{
    public class CommonConstant
    {
        public static class ErrorCode
        {
            public static readonly int INVALID_TOKEN = 510;
        }

        public static class RetrievalStatus
        {
            public const string created = "Created"; //upon creation of retrieval form
            public const string retrieved = "Retrieved"; //after clicking retrieved
        }

        public static class AdjVoucherStatus
        {
            public static readonly string CREATED = "Created";
            public static readonly string PENDING_APPROVE = "Pending Approval"; //upon creation, but not approved by anyone
            public static readonly string PENDING_MANAGER_APPROVE = "Pending Manager Approval"; //if anything more than $250, and has been approved by supervisor but not yet approved by manager
            public static readonly string APPROVED = "Approved"; // if anything <250 and approved by supervisor, OR if anything more >250 and both supervisor and manager approved 
            public static readonly string REJECTED = "Rejected";
        }

        public static class ROLE
        {
            public static readonly string STORE_CLERK = "sc";
            public static readonly string STORE_SUPERVISOR = "ss";
            public static readonly string STORE_MANAGER = "sm";
            public static readonly string DEPARTMENT_HEAD = "dh";
            public static readonly string DEPARTMENT_EMPLOYEE = "de";
            public static readonly string DEPARTMENT_REPRESENTATIVE = "dr";
            public static readonly string DEPARTMENT_DELEGATE = "dd";
        }

        public static Dictionary<string, string> ROLE_NAME = new Dictionary<string, string>()
        {
            {ROLE.STORE_CLERK,"Clerk" },
            {ROLE.STORE_MANAGER,"Manager" },
            {ROLE.STORE_SUPERVISOR,"Supervisor" },
            {ROLE.DEPARTMENT_EMPLOYEE,"Department Employee"},
            {ROLE.DEPARTMENT_HEAD,"Department Head"},
            {ROLE.DEPARTMENT_REPRESENTATIVE,"Department Representative"},
            {ROLE.DEPARTMENT_DELEGATE,"Department Delegate"}
        };

        public static class PurchaseOrderDetailStatus
        {
            public const string pending = "Pending Delivery"; //initial status upon creation
            public const string received = "Received"; //received from supplier
        }
        public static class PurchaseOrderStatus
        {
            public const string pending = "Pending Delivery"; //When >1 PurchaseOrderDetail is still pending delivery
            public const string completed = "Completed"; //When all PurchaseOrderDetail is received
        }

        public static class RequsitionStatus
        {
            public const string created = "Created"; //upon creation before approval
            public const string pendapprov = "Pending Approval"; //after submit for approval
            public const string approved = "Approved"; // after approved, before clerk enters a disbursement time
            public const string rejected = "Rejected";
            public const string confirmed = "Confirmed"; //after approved, and after clerk entered a disbursement time
            public const string received = "Received"; // after department rep received at collection point, and pressed the button to indicate he received. This will trigger the button for clerk to click acknowledged
            public const string completed = "Completed"; //after clerk click acknowledged, requsition status is completed
        }

        public static class PurchaseRequestStatus
        {
            public const string created = "Created"; //upon creation before approval
            public const string pendapprov = "Pending Approval"; //after submit for approval
            public const string rejected = "Rejected";
            public const string approved = "Approved";
        }
    }
}
