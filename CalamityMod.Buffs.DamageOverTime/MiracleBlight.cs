using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CalamityMod.DataStructures;
using CalamityMod.NPCs;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.Particles;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class MiracleBlight : ModBuff
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static hook_DrawNPC _003C0_003E__DrawForNPC;
	}

	public static DebuffData debuffData = new DebuffData
	{
		EnemyLostRegen = 3000f
	};

	[CompilerGenerated]
	private static Asset<Texture2D> _003CShaderTexture_003Ek__BackingField;

	public static Asset<Texture2D> ShaderTexture => _003CShaderTexture_003Ek__BackingField ?? (_003CShaderTexture_003Ek__BackingField = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons", (AssetRequestMode)2));

	public static List<int> ExcludedNPCsForShader => new List<int>
	{
		ModContent.NPCType<AresBody>(),
		ModContent.NPCType<AresGaussNuke>(),
		ModContent.NPCType<AresLaserCannon>(),
		ModContent.NPCType<AresPlasmaFlamethrower>(),
		ModContent.NPCType<AresTeslaCannon>(),
		398,
		397,
		396
	};

	public override void Load()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		object obj = _003C_003EO._003C0_003E__DrawForNPC;
		if (obj == null)
		{
			hook_DrawNPC val = DrawForNPC;
			_003C_003EO._003C0_003E__DrawForNPC = val;
			obj = (object)val;
		}
		On_Main.DrawNPC += (hook_DrawNPC)obj;
	}

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = debuffData;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().miracleBlight = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().miracleBlight = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		Color sparkColor = (Color)(Main.rand.Next(4) switch
		{
			0 => Color.Red, 
			1 => Color.MediumTurquoise, 
			2 => Color.Orange, 
			_ => Color.LawnGreen, 
		});
		if (Main.rand.NextBool(2))
		{
			Dust dust = Dust.NewDustPerfect(Player.Calamity().RandomDebuffVisualSpot, 66, CalamityUtils.RandomVelocity(100f, 70f, 150f, 0.04f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.7f, 0.85f);
			dust.color = sparkColor;
		}
		float numberOfDusts = 1f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			MathHelper.ToRadians((float)i * rotFactor);
			Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 150f, 0.04f);
			velOffset *= Main.rand.NextFloat(2f, 13f);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(Player.Center + Player.velocity * 3f + velOffset * 1.5f, -velOffset * 0.25f, affectedByGravity: false, 4, 0.008f, sparkColor, new Vector2(0.6f, 1.2f)));
		}
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcSize = npc.Center + new Vector2(Main.rand.NextFloat(-npc.width / 2, npc.width / 2), Main.rand.NextFloat(-npc.height / 2, npc.height / 2));
		Color sparkColor = (Color)(Main.rand.Next(4) switch
		{
			0 => Color.Red, 
			1 => Color.MediumTurquoise, 
			2 => Color.Orange, 
			_ => Color.LawnGreen, 
		});
		if (Main.rand.NextBool(4))
		{
			Dust dust = Dust.NewDustPerfect(npcSize, 66, CalamityUtils.RandomVelocity(100f, 70f, 150f, 0.04f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.7f, 0.85f) + 7E-07f * (float)npc.width * (float)npc.height;
			dust.color = sparkColor;
		}
		float rotFactor = 360f;
		if (Main.rand.NextBool(3))
		{
			MathHelper.ToRadians(rotFactor);
			Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 150f, 0.04f);
			velOffset *= Main.rand.NextFloat(5f, 9f) + 0.0002f * (float)npc.width * (float)npc.height;
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(npc.Center + npc.velocity * 3f + velOffset * 1.5f, -velOffset * 0.25f, affectedByGravity: false, 4, 0.008f, sparkColor, new Vector2(0.6f, 1.2f)));
		}
	}

	private static void DrawForNPC(orig_DrawNPC orig, Main self, int iNPCIndex, bool behindTiles)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = Main.npc[iNPCIndex];
		if (!CalamityDrawParameterNPC.DrawingMiracleBlight[npc.whoAmI])
		{
			orig.Invoke(self, iNPCIndex, behindTiles);
			return;
		}
		if (ExcludedNPCsForShader.Contains(npc.type))
		{
			orig.Invoke(self, iNPCIndex, behindTiles);
			return;
		}
		using (Main.spriteBatch.Scope())
		{
			using RenderTargetLease lease = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice);
			using (lease.Scope(preserveContents: true, Color.Transparent))
			{
				Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.Transform);
				orig.Invoke(self, iNPCIndex, behindTiles);
				Main.spriteBatch.End();
			}
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
			MiscShaderData miscShaderData = GameShaders.Misc["CalamityMod:MiracleBlight"];
			miscShaderData.UseImage1(ShaderTexture);
			miscShaderData.UseOpacity(0.7f);
			miscShaderData.Apply();
			Main.spriteBatch.Draw((Texture2D)(object)lease.Target, Vector2.Zero, Color.White);
			Main.spriteBatch.End();
		}
	}
}
