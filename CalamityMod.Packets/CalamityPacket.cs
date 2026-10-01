using System;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal abstract class CalamityPacket : ILoadable
{
	private ushort _NetID;

	private PropertyInfo _Prop_Static_Instance;

	public abstract void HandlePacket(BinaryReader packet, int sender);

	public void Load(Mod mod)
	{
		_NetID = CalamityNetcode.RegisterHandler(this);
		Type type = GetType();
		PropertyInfo instanceProperty = type.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		if (!(instanceProperty == null))
		{
			if (!instanceProperty.PropertyType.IsAssignableFrom(type))
			{
				CalamityMod.Log.Error((object)("Packet instance's 'Instance' property is not asssignable with given type! [Failed On: '" + type.FullName + "']"));
			}
			instanceProperty.SetValue(null, this);
			_Prop_Static_Instance = instanceProperty;
		}
	}

	public virtual void Unload()
	{
		_Prop_Static_Instance?.SetValue(null, null);
		_Prop_Static_Instance = null;
	}

	public void CloneAndBroadcast(BinaryReader packet, long startIndex, int length, int ignoreClient = -1)
	{
		if (Main.dedServ && startIndex >= 0)
		{
			packet.BaseStream.Position = startIndex;
			Span<byte> span = ((length > 256) ? ((Span<byte>)new byte[length]) : stackalloc byte[length]);
			Span<byte> buffer = span;
			packet.BaseStream.Read(buffer);
			ModPacket modPacket = CreateBasePacket();
			modPacket.Write(buffer);
			modPacket.Send(ignoreClient);
		}
	}

	public ModPacket CreateBasePacket()
	{
		ModPacket packet = CalamityMod.Instance.GetPacket();
		CalamityNetcode.WriteHandlerNetID(packet, _NetID);
		return packet;
	}
}
