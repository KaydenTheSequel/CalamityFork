using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Packets.Entities;
using CalamityMod.Projectiles.Boss.BrainOfCthulhu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;

[AutoloadBossHead]
public class FalseBrain : ModNPC, ILocalizedModType, IModType
{
	internal bool BeenHit;

	private int SpawnTime = 60;

	internal static List<int> blackListedProjectiles = new List<int>();

	public new string LocalizationCategory => "NPCs";

	public override string BossHeadTexture => "Terraria/Images/NPC_Head_Boss_23";

	private int Variant => (int)base.NPC.localAI[0];

	private float Angle => base.NPC.ai[0];

	private ref float Time => ref base.NPC.ai[1];

	private int AttackTime
	{
		get
		{
			return (int)base.NPC.ai[2];
		}
		set
		{
			base.NPC.ai[2] = value;
		}
	}

	internal static float TimeDivisor => 360f;

	internal float DrawPriority => (float)Math.Cos((0f - Time) * ((float)Math.PI * 2f) / TimeDivisor);

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 160;
		base.NPC.height = 110;
		base.NPC.damage = 0;
		base.NPC.lifeMax = 1;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.boss = true;
		base.NPC.immortal = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.chaseable = false;
		base.NPC.npcSlots = 0f;
		base.NPC.netAlways = true;
		base.NPC.ShowNameOnHover = false;
		base.Music = 13;
		base.SceneEffectPriority = (SceneEffectPriority)(-1);
		base.NPC.localAI[0] = Main.rand.Next(Main.getGoodWorld ? 7 : 6);
		base.NPC.localAI[1] = 1f + Main.rand.NextFloat(-0.25f, 0.25f);
	}

	public override void AI()
	{
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		if (NPC.crimsonBoss == -1)
		{
			base.NPC.active = false;
			return;
		}
		NPC brain = Main.npc[NPC.crimsonBoss];
		base.NPC.GivenName = brain.GivenOrTypeName + $": {brain.life}/{brain.lifeMax}";
		if (brain.AIOverride<BrainOfCthulhuAI>().AttackFlag)
		{
			BeenHit = true;
		}
		if (BeenHit)
		{
			base.NPC.dontTakeDamage = true;
			if (AttackTime++ >= 60)
			{
				base.NPC.active = false;
			}
		}
		else
		{
			float lerp = CalamityUtils.SineInOutEasing(MathHelper.Clamp((float)SpawnTime / -30f, 0f, 1f), 1);
			float baseDist = 240f;
			float circleDist = 480f;
			if (SpawnTime != -30)
			{
				baseDist = MathHelper.Lerp(480f, 240f, lerp);
				circleDist = MathHelper.Lerp(240f, 480f, lerp);
			}
			base.NPC.Center = brain.AIOverride<BrainOfCthulhuAI>().AttackPosition + Vector2.UnitX.RotatedBy(Angle) * (baseDist + circleDist * ((float)Math.Sin((0f - Time) * ((float)Math.PI * 2f) / TimeDivisor) / 2f + 0.5f));
			NPC nPC = base.NPC;
			nPC.Center += Vector2.UnitX.RotatedBy(Angle + (float)Math.PI / 2f) * (90f * (float)Math.Cos(Time * ((float)Math.PI * 2f) / TimeDivisor));
		}
		if (SpawnTime > 0)
		{
			SpawnTime--;
		}
		else if (SpawnTime >= -30)
		{
			Time += (float)(--SpawnTime) / -30f;
			if (SpawnTime <= -30)
			{
				base.NPC.dontTakeDamage = false;
			}
		}
		else
		{
			Time++;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(BeenHit);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		BeenHit = reader.ReadBoolean();
	}

	public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (!BeenHit)
		{
			if (Main.netMode == 0)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<TelekineticBlast>(), 50, 0.5f, -1, player.whoAmI, 8f, base.NPC.whoAmI);
			}
			else
			{
				BrainIllusionHitPacket.Send(base.NPC.whoAmI, player.whoAmI);
			}
			base.NPC.dontTakeDamage = true;
		}
	}

	public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if (!BeenHit && !projectile.Calamity().IgnoreBoCIllusions && projectile.owner != -1)
		{
			if (Main.netMode == 0)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<TelekineticBlast>(), 50, 0.5f, -1, projectile.owner, 8f, base.NPC.whoAmI);
			}
			else
			{
				BrainIllusionHitPacket.Send(base.NPC.whoAmI, projectile.owner);
			}
			base.NPC.dontTakeDamage = true;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.2f * base.NPC.localAI[1];
		if ((int)base.NPC.frameCounter > 3)
		{
			base.NPC.frameCounter = 0.0;
		}
	}

	internal void DrawSelf(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scaleDistort = default(Vector2);
		((Vector2)(ref scaleDistort))._002Ector((float)Math.Cos(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) * 2f) / 2f, (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) * 2f) / 2f);
		Vector2 scaleAddition = Vector2.zeroVector;
		float endLerp = (float)AttackTime / 60f;
		float startLerp = (float)SpawnTime / 60f;
		Texture2D tex;
		Rectangle frame;
		if (BrainOfCthulhuSystem.IsBrainOfCthulhuTextureVanilla)
		{
			tex = TextureAssets.Npc[base.Type].Value;
			frame = tex.Frame(7, 4, Variant, (int)base.NPC.frameCounter);
		}
		else
		{
			tex = TextureAssets.Npc[266].Value;
			frame = tex.Frame(1, 8, 0, (int)base.NPC.frameCounter);
		}
		if (SpawnTime > 0)
		{
			drawColor *= 1f - startLerp;
			scaleDistort *= startLerp;
		}
		else if (AttackTime > 0)
		{
			drawColor = Color.Lerp(drawColor, Color.Red, endLerp) * (1f - endLerp);
			scaleDistort *= endLerp;
		}
		else
		{
			scaleDistort = Vector2.Zero;
		}
		spriteBatch.Draw(tex, base.NPC.Center + Vector2.UnitY * 16f - screenPos, (Rectangle?)frame, drawColor, base.NPC.rotation, frame.Size() * 0.5f, (Vector2.One + scaleAddition + scaleDistort) * base.NPC.scale, (SpriteEffects)0, 0f);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		return false;
	}
}
