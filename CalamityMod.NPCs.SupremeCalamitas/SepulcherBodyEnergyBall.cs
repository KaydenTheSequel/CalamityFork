using System;
using System.IO;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SupremeCalamitas;

public class SepulcherBodyEnergyBall : ModNPC
{
	private bool setAlpha;

	public int NoStartAttack = 240;

	public NPC AheadSegment => Main.npc[(int)base.NPC.ai[1]];

	public NPC HeadSegment => Main.npc[(int)base.NPC.ai[2]];

	public ref float AttackTimer => ref base.NPC.localAI[0];

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.SepulcherHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 20;
		base.NPC.height = 20;
		base.NPC.lifeMax = (CalamityWorld.revenge ? 345000 : 300000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.scale *= (Main.expertMode ? 1.35f : 1.2f);
		base.NPC.alpha = 255;
		base.NPC.chaseable = false;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.canGhostHeal = false;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		calamityGlobalNPC.DR = 0.999999f;
		calamityGlobalNPC.unbreakableDR = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(AttackTimer);
		writer.Write(setAlpha);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		AttackTimer = reader.ReadSingle();
		setAlpha = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		NoStartAttack--;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		bool shouldDie = false;
		if (base.NPC.ai[1] <= 0f)
		{
			shouldDie = true;
		}
		else if (AheadSegment.life <= 0 || !AheadSegment.active || base.NPC.life <= 0)
		{
			shouldDie = true;
		}
		if (shouldDie)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
		}
		if (AheadSegment.alpha < 128 && !setAlpha)
		{
			if (base.NPC.alpha != 0)
			{
				for (int i = 0; i < 2; i++)
				{
					Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
					dust.noGravity = true;
					dust.noLight = true;
				}
			}
			base.NPC.alpha -= 42;
			if (base.NPC.alpha <= 0)
			{
				setAlpha = true;
				base.NPC.alpha = 0;
			}
		}
		else
		{
			base.NPC.alpha = HeadSegment.alpha;
		}
		if (Main.npc.IndexInRange((int)base.NPC.ai[1]))
		{
			Vector2 offsetToAheadSegment = AheadSegment.Center - base.NPC.Center;
			base.NPC.rotation = offsetToAheadSegment.ToRotation() + (float)Math.PI / 2f;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.Center = AheadSegment.Center - offsetToAheadSegment.SafeNormalize(Vector2.UnitY) * 34f;
			base.NPC.spriteDirection = (offsetToAheadSegment.X > 0f).ToDirectionInt();
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		base.NPC.frame.Y = ((int)(base.NPC.frameCounter / 5.0) + base.NPC.whoAmI) % Main.npcFrameCount[base.Type] * frameHeight;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1 || base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < Main.rand.Next(1, 4); i++)
		{
			if (Main.rand.NextBool(3))
			{
				Vector2 soulVelocity = -Vector2.UnitY.RotatedByRandom(0.5299999713897705) * Main.rand.NextFloat(2.5f, 4f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, soulVelocity, ModContent.ProjectileType<SepulcherSoul>(), 0, 0f);
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}
}
