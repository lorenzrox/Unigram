// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Channels
{
	/// <summary>
	/// RCP method channels.updateUsername
	/// </summary>
	public partial class TLChannelsUpdateUsername : TLObject
	{
		public TLInputChannelBase Channel { get; set; }
		public String Username { get; set; }

		public TLChannelsUpdateUsername() { }
		public TLChannelsUpdateUsername(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.ChannelsUpdateUsername; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Channel = TLFactory.Read<TLInputChannelBase>(from, cache);
			Username = from.ReadString();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x3514B3DE);
			to.WriteObject(Channel, cache);
			to.Write(Username);
			if (cache) WriteToCache(to);
		}
	}
}