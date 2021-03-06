namespace EnergyTrading.MDM.Contracts.Sample
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    using EnergyTrading.Contracts.Atom;
    using EnergyTrading.Mdm.Contracts;

    [DataContract(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlRoot(Namespace = "http://schemas.rwe.com/nexus")]
    [XmlType(Namespace = "http://schemas.rwe.com/nexus")]
    public class PartyRole : IMdmEntity
    {
        public PartyRole()
        {
            this.Identifiers = new MdmIdList();
            this.Details = new PartyRoleDetails();
            this.Links = new List<Link>();
        }

        [DataMember(Order = 4, EmitDefaultValue = false)]
        [XmlElement]
        public Audit Audit { get; set; }

        [DataMember(Order = 2)]
        [XmlElement]
        public PartyRoleDetails Details { get; set; }

        [DataMember(Order = 1)]
        [XmlArray]
        [XmlArrayItem("ReferenceID")]
        public MdmIdList Identifiers { get; set; }

        [DataMember(Order = 7, EmitDefaultValue = false)]
        [XmlElement("link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Link> Links { get; set; }

        [DataMember(Order = 3, EmitDefaultValue = false)]
        [XmlElement]
        public SystemData MdmSystemData { get; set; }

        [DataMember(Order = 6)]
        [XmlElement]
        public EntityId Party { get; set; }

        [DataMember(Order = 5)]
        [XmlElement]
        public string PartyRoleType { get; set; }

        object IMdmEntity.Details
        {
            get
            {
                return this.Details;
            }

            set
            {
                this.Details = (PartyRoleDetails)value;
            }
        }
    }
}