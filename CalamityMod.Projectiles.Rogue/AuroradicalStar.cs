using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class AuroradicalStar : ModProjectile, ILocalizedModType, IModType
{
	public int[] dustTypes = new int[2]
	{
		ModContent.DustType<AstralBlue>(),
		ModContent.DustType<AstralOrange>()
	};

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 100;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 360;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.5f, 0.1f);
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 20 + Main.rand.Next(40);
			if (Main.rand.NextBool(5))
			{
				SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.position);
			}
		}
		float scaleAmt = (float)(int)Main.mouseTextColor / 200f - 0.35f;
		scaleAmt *= 0.2f;
		base.Projectile.scale = scaleAmt + 0.95f;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 15f)
		{
			int astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.Next(dustTypes), 0f, 0f, 100, default(Color), 0.8f);
			Main.dust[astral].noGravity = true;
			Dust obj = Main.dust[astral];
			obj.velocity *= 0f;
		}
		float maxDistance = 800f;
		int targetIndex = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy(base.Projectile))
			{
				float extraDistance = n.width / 2 + n.height / 2;
				if (Vector2.Distance(n.Center, base.Projectile.Center) < maxDistance + extraDistance)
				{
					targetIndex = n.whoAmI;
					break;
				}
			}
		}
		if (targetIndex == -1)
		{
			return;
		}
		Vector2 targetVec = Main.npc[targetIndex].Center - base.Projectile.Center;
		if (base.Projectile.ai[0] >= 30f)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] < 90f)
			{
				float speedMult = 16f;
				((Vector2)(ref targetVec)).Normalize();
				targetVec *= speedMult;
				base.Projectile.velocity = (base.Projectile.velocity * 15f + targetVec) / 16f;
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile = base.Projectile;
				projectile.velocity *= speedMult;
			}
			else
			{
				base.Projectile.velocity = (Main.npc[targetIndex].Center - base.Projectile.Center) / 12f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		OnHitEffect(target);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		OnHitEffect(target);
	}

	private void OnHitEffect(Entity target)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike && Main.myPlayer == base.Projectile.owner)
		{
			Vector2 pos = default(Vector2);
			((Vector2)(ref pos))._002Ector(target.Center.X + (float)Main.rand.Next(-201, 201), Main.screenPosition.Y - 600f - (float)Main.rand.Next(50));
			Vector2 meteorVel = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(pos, target, 20f, 3);
			int comet = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos, meteorVel, ModContent.ProjectileType<LeonidCometBig>(), (int)((float)base.Projectile.damage * 1.25f), base.Projectile.knockBack, base.Projectile.owner);
			if (comet.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[comet].DamageType = RogueDamageClass.Instance;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.position);
		for (int d = 0; d < 2; d++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.Next(dustTypes), 0f, 0f, 50);
		}
		for (int i = 0; i < 20; i++)
		{
			int astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.Next(dustTypes), 0f, 0f, 0, default(Color), 1.5f);
			Main.dust[astral].noGravity = true;
			Dust obj = Main.dust[astral];
			obj.velocity *= 3f;
			astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 50);
			Dust obj2 = Main.dust[astral];
			obj2.velocity *= 2f;
			Main.dust[astral].noGravity = true;
		}
		if (!Main.dedServ)
		{
			for (int g = 0; g < 3; g++)
			{
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, new Vector2(base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}
}
