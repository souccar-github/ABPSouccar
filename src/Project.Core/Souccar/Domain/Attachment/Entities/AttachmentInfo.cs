using System;
using Project.Souccar.Domain.Attachment.Enums;
using Project.Souccar.Domain.DomainModel;

namespace Project.Souccar.Domain.Attachment.Entities
{
    public class AttachmentInfo : SouccarAggregate
    {
        public virtual string Path { get; set; }
        public virtual string PhysicalFileName { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual string ModelFullClassName { get; set; }
        public virtual DateTime UploadDate { get; set; }
        public virtual string Description { get; set; }
        public virtual EntityType EntityType { get; set; }
        //public virtual int BaseId { get; set; }
    }
}