// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLInputMessagesFilterMusic : TLMessagesFilterBase 
	{
		public TLInputMessagesFilterMusic() { }
		public TLInputMessagesFilterMusic(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.InputMessagesFilterMusic; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x3751B49E);
			if (cache) WriteToCache(to);
		}
	}
}