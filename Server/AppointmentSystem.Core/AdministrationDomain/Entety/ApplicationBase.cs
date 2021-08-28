namespace AppointmentSystem.Core.AdministrationDomain.Entety
{
    using AppointmentSystem.Core.Entities.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ApplicationBase : DeletableEntity
    {
        private List<AdministationInformationEntry> administationInformationEntryCatalog
            = new List<AdministationInformationEntry>();
        public ApplicationBase()
        {
            //EF
        }
        public ApplicationBase(string accountId)
        {
            this.AccountId = accountId;
        }
        public int Id { get; set; }

        public string AccountId { get; set; }

        public Status CurentStatus
        {
            get
            {
                var CurentStatus = this.administationInformationEntryCatalog
                       .Where(ai => ai.Status != Status.information)
                       .OrderBy(ai => ai.CreatedOn)
                       .FirstOrDefault();
                if(CurentStatus != null)
                {
                    return CurentStatus.Status;
                }
                throw new ApplicationException();// make custom Exeption
            }
        }

        public  IReadOnlyCollection<AdministationInformationEntry> AdministationInformationEntryCatalog 
            => this.administationInformationEntryCatalog.AsReadOnly();

        public void AddInformationAdministationInformationEntry(AdministationInformationEntry administationInformationEntry)
            => this.AddAdministationInformationEntry(administationInformationEntry);
        protected void AddAdministationInformationEntry(AdministationInformationEntry administationInformationEntry) 
        {
            this.ChekAdministationInformationEntryCatalogForValidStatus(administationInformationEntry);
            
            if (!this.administationInformationEntryCatalog.Any(ai => ai.Id == administationInformationEntry.Id))
            {
                this.administationInformationEntryCatalog.Add(administationInformationEntry);
            }//can thorw Custom Exeption
        }
        public AdministationInformationEntry GetAdministationInformationEntryById(int id)
        => this.administationInformationEntryCatalog.FirstOrDefault(ai => ai.Id == id);

        public abstract void Approve(AdministationInformationEntry administationInformationEntry);//in futere raise domain -> events and also provides encapsulation over AddAdministationInformationEntry

        public abstract void Rejected(AdministationInformationEntry administationInformationEntry);//read Approvve

        private bool ChekAdministationInformationEntryCatalogForValidStatus(AdministationInformationEntry administationInformationEntry)
        {
            if (administationInformationEntry.Status == Status.information)//if administationInformationEntry is with  informataion status  can be appended with no restrictions
            {
                return true;
            }

            if (this.administationInformationEntryCatalog.Count == 0 && administationInformationEntry.Status != Status.pending)// if administationInformationEntry is first Status must be pending
            {
                throw new ApplicationException();//make custom Exeption
            }

            if( this.CurentStatus == Status.pending  // canot Add Pending administationInformationEntry if curent Status is Panding
                && administationInformationEntry.Status == Status.pending)
            {
                throw new ApplicationException();//make custom Exeption
            }
            if ((this.CurentStatus == Status.approved 
                || this.CurentStatus == Status.rejected) //if administationInformationEntry is with final status(approved, canceld) only information messages can be added
                && (administationInformationEntry.Status == Status.rejected 
                     || administationInformationEntry.Status == Status.pending
                     || administationInformationEntry.Status == Status.approved))
            {
                throw new ApplicationException();//make custom Exeption
            }
            if (this.CurentStatus != Status.pending 
                && administationInformationEntry.Status == Status.update)// can only update administationInformationEntry with status pending 
            {
                throw new ApplicationException();// mak custom Exeption
            }
            return true;
        }

    }
    
}
