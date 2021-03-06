// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLAuthSentCode : TLObject 
	{
		[Flags]
		public enum Flag : Int32
		{
			PhoneRegistered = (1 << 0),
			NextType = (1 << 1),
			Timeout = (1 << 2),
		}

		public bool IsPhoneRegistered { get { return Flags.HasFlag(Flag.PhoneRegistered); } set { Flags = value ? (Flags | Flag.PhoneRegistered) : (Flags & ~Flag.PhoneRegistered); } }
		public bool HasNextType { get { return Flags.HasFlag(Flag.NextType); } set { Flags = value ? (Flags | Flag.NextType) : (Flags & ~Flag.NextType); } }
		public bool HasTimeout { get { return Flags.HasFlag(Flag.Timeout); } set { Flags = value ? (Flags | Flag.Timeout) : (Flags & ~Flag.Timeout); } }

		public Flag Flags { get; set; }
		public TLAuthSentCodeTypeBase Type { get; set; }
		public String PhoneCodeHash { get; set; }
		public TLAuthCodeTypeBase NextType { get; set; }
		public Int32? Timeout { get; set; }

		public TLAuthSentCode() { }
		public TLAuthSentCode(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.AuthSentCode; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Flags = (Flag)from.ReadInt32();
			Type = TLFactory.Read<TLAuthSentCodeTypeBase>(from, cache);
			PhoneCodeHash = from.ReadString();
			if (HasNextType) NextType = TLFactory.Read<TLAuthCodeTypeBase>(from, cache);
			if (HasTimeout) Timeout = from.ReadInt32();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			UpdateFlags();

			to.Write(0x5E002502);
			to.Write((Int32)Flags);
			to.WriteObject(Type, cache);
			to.Write(PhoneCodeHash);
			if (HasNextType) to.WriteObject(NextType, cache);
			if (HasTimeout) to.Write(Timeout.Value);
			if (cache) WriteToCache(to);
		}

		private void UpdateFlags()
		{
			HasNextType = NextType != null;
			HasTimeout = Timeout != null;
		}
	}
}