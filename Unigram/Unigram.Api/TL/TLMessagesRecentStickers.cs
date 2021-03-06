// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLMessagesRecentStickers : TLMessagesRecentStickersBase 
	{
		public Int32 Hash { get; set; }
		public TLVector<TLDocumentBase> Stickers { get; set; }

		public TLMessagesRecentStickers() { }
		public TLMessagesRecentStickers(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.MessagesRecentStickers; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Hash = from.ReadInt32();
			Stickers = TLFactory.Read<TLVector<TLDocumentBase>>(from, cache);
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x5CE20970);
			to.Write(Hash);
			to.WriteObject(Stickers, cache);
			if (cache) WriteToCache(to);
		}
	}
}