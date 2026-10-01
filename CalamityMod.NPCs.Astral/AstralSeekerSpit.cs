using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class AstralSeekerSpit : ModNPC
{
	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.ProjectileNPC[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 16;
		base.NPC.height = 16;
		base.NPC.damage = 45;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 1;
		base.NPC.HitSound = null;
		base.NPC.DeathSound = SoundID.NPCDeath9;
		base.NPC.noGravity = true;
		base.NPC.knockBackResist = 0f;
		base.NPC.noTileCollide = true;
		base.NPC.alpha = 80;
		base.NPC.aiStyle = -1;
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 75;
		}
	}

	public override void AI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.ai[0] += 0.18f;
		float f = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
		float pulse = (float)Math.Sin(base.NPC.ai[0]);
		float radius = 5.8f;
		Vector2 offset = f.ToRotationVector2() * pulse * radius;
		Dust.NewDustPerfect(base.NPC.Center + offset, ModContent.DustType<AstralOrange>(), Vector2.Zero);
		Dust.NewDustPerfect(base.NPC.Center - offset, ModContent.DustType<AstralBlue>(), Vector2.Zero);
		if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height) && Main.netMode != 1)
		{
			base.NPC.StrikeInstantKill();
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (modifiers.GetDamage(base.NPC.damage, crit: false) > 0 && Main.netMode != 1)
		{
			base.NPC.StrikeInstantKill();
		}
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		if (modifiers.GetDamage(base.NPC.damage, 0f, 0f) > 0f)
		{
			int parent = ModContent.NPCType<SightseerSpitter>();
			if (target.HasNPCBannerBuff(parent))
			{
				ItemID.BannerEffect effect = ItemID.Sets.BannerStrength[Item.BannerToItem(parent)];
				modifiers.IncomingDamageMultiplier *= (Main.expertMode ? effect.ExpertDamageReceived : effect.NormalDamageReceived);
			}
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		DoKillDust();
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		}
	}

	private void DoKillDust()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		int numDust = Main.rand.Next(17, 25);
		float rotPerIter = (float)Math.PI * 2f / (float)numDust;
		float angle = 0f;
		for (int i = 0; i < numDust; i++)
		{
			Vector2 vel = (angle + Main.rand.NextFloat(-0.04f, 0.04f)).ToRotationVector2();
			int dustType = (Main.rand.NextBool() ? ModContent.DustType<AstralOrange>() : ModContent.DustType<AstralBlue>());
			Dust.NewDustPerfect(base.NPC.Center, dustType, vel * Main.rand.NextFloat(1.8f, 2.2f)).customData = base.NPC;
			angle += rotPerIter;
		}
	}
}
