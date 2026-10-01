using System;
using System.IO;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class SacrificeProjectile : ModProjectile, ILocalizedModType, IModType
{
	public bool AbleToHealOwner = true;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool StickingToAnything => base.Projectile.ai[0] == 1f;

	public bool ReturningToOwner => base.Projectile.ai[0] == 2f;

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Sacrifice";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 62);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(AbleToHealOwner);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		AbleToHealOwner = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().LocketClone)
		{
			AbleToHealOwner = false;
		}
		if (ReturningToOwner)
		{
			base.Projectile.timeLeft = 20;
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(Owner.Center) * 28f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI + (float)Math.PI / 4f;
			base.Projectile.damage = 0;
			Rectangle hitbox = base.Projectile.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(Owner.Hitbox))
			{
				if (AbleToHealOwner)
				{
					Owner.DoLifestealDirect(null, base.Projectile.Calamity().stealthStrike ? 40 : 3, 0.4f);
				}
				base.Projectile.Kill();
			}
		}
		else if (!StickingToAnything)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		}
		else if (!Main.dedServ && (float)base.Projectile.timeLeft % 40f == 39f)
		{
			for (int i = 0; i < 60; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(50f, 50f), 267);
				dust.velocity = Main.rand.NextVector2Circular(3f, 3f);
				dust.noGravity = true;
				dust.color = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat(0.25f, 1f));
				dust.scale = Main.rand.NextFloat(1f, 1.4f);
			}
		}
		if (StickingToAnything)
		{
			if (base.Projectile.timeLeft > 90 && !base.Projectile.Calamity().stealthStrike)
			{
				base.Projectile.timeLeft = 90;
			}
			else if (base.Projectile.timeLeft > 180 && base.Projectile.Calamity().stealthStrike)
			{
				base.Projectile.timeLeft = 180;
			}
		}
		base.Projectile.StickyProjAI(50);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ModifyHitNPCSticky(15);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.5f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 5, 0f, 0f, 0, default(Color), 1.5f);
			dust.position += base.Projectile.velocity.SafeNormalize(Vector2.Zero) * base.Projectile.scale * 42f;
			dust.noGravity = true;
		}
	}
}
