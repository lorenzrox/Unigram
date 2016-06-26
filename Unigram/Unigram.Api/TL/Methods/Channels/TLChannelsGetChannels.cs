// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Channels
{
	/// <summary>
	/// RCP method channels.getChannels
	/// </summary>
	public partial class TLChannelsGetChannels : TLObject
	{
		public TLVector<TLInputChannelBase> Id { get; set; }

		public TLChannelsGetChannels() { }
		public TLChannelsGetChannels(TLBinaryReader from, TLType type = TLType.ChannelsGetChannels)
		{
			Read(from, type);
		}

		public override TLType TypeId { get { return TLType.ChannelsGetChannels; } }

		public override void Read(TLBinaryReader from, TLType type = TLType.ChannelsGetChannels)
		{
			Id = TLFactory.Read<TLVector<TLInputChannelBase>>(from);
		}

		public override void Write(TLBinaryWriter to)
		{
			to.Write(0xA7F6BBB);
			to.WriteObject(Id);
		}
	}
}