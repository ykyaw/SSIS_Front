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

        public static class RetrievalFormStatus
        {
            public static readonly string CREATING = "creating";
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
        }

        public static Dictionary<string, string> ROLE_NAME = new Dictionary<string, string>()
        {
            {ROLE.STORE_CLERK,"Clerk" },
            {ROLE.STORE_MANAGER,"Manager" },
            {ROLE.STORE_SUPERVISOR,"Supervisor" },
            {ROLE.DEPARTMENT_EMPLOYEE,"Department Employee"},
            {ROLE.DEPARTMENT_HEAD,"Department Head"},
            {ROLE.DEPARTMENT_REPRESENTATIVE,"Department Representative"}
        };

        public static class PurchaseOrderStatus
        {
            public const string approved = "Approved";
            public const string received = "Received"; //received from supplier
        }
    }
}
