using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ChronomancersScytheHoldout : ModProjectile, ILocalizedModType, IModType
{
	public static int ClockChance = 30;

	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Items/Weapons/Magic/ChronomancersScythe";

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.ownerHitCheck = true;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 130f * base.Projectile.scale;
		float bladeWidth = 90f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * bladeLength, bladeWidth, ref collisionPoint);
	}

	public override void AI()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		Owner.ChangeDir((int)base.Projectile.ai[2]);
		Player player = Main.player[base.Projectile.owner];
		if (player == null || player.dead)
		{
			base.Projectile.Kill();
		}
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.rotation += (float)Math.PI / 30f * base.Projectile.ai[2];
		UpdateOwnerVars();
		base.Projectile.Center = Owner.MountedCenter + base.Projectile.rotation.ToRotationVector2() * 10f - Vector2.UnitX * 4f * (float)Owner.direction;
		if ((base.Projectile.rotation > 5.497787f && base.Projectile.ai[2] == 1f) || (base.Projectile.rotation < -5.497787f && base.Projectile.ai[2] == -1f))
		{
			base.Projectile.Kill();
		}
		base.Projectile.ai[0]++;
		float HandInterval = 5f;
		if (base.Projectile.ai[0] >= HandInterval)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				float aivar = ((base.Projectile.ai[2] == -1f) ? (1f - base.Projectile.ai[1] - 1f) : base.Projectile.ai[1]);
				Vector2 spawnPos = Main.player[base.Projectile.owner].Center + ((float)Math.PI / 6f * aivar - (float)Math.PI / 2f).ToRotationVector2() * 160f;
				Vector2 rotationVector = spawnPos - Main.player[base.Projectile.owner].Center;
				int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos, rotationVector * 5f, ModContent.ProjectileType<ChronoIcicleLarge>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2] * (base.Projectile.ai[1] + 1f));
				Main.projectile[p].rotation = (spawnPos - Main.player[base.Projectile.owner].Center).ToRotation() + (float)Math.PI / 2f;
			}
			base.Projectile.ai[1]++;
			base.Projectile.ai[0] = 0f;
		}
	}

	public void UpdateOwnerVars()
	{
		float rotOffset = ((base.Projectile.ai[2] == -1f) ? ((float)Math.PI * 3f) : ((float)Math.PI / 2f));
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.rotation - rotOffset);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<TimeDistortion>(), 60);
		if (Main.rand.NextBool(ClockChance))
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Main.rand.NextVector2Circular(-2f, 2f), ModContent.ProjectileType<ChronoClock>(), 0, 0f, base.Projectile.owner);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = Owner.direction;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		Owner.ChangeDir((int)base.Projectile.ai[2]);
		Texture2D scytheTexture = TextureAssets.Projectile[base.Type].Value;
		Vector2 handleOrigin = ((base.Projectile.ai[2] == -1f) ? new Vector2((float)scytheTexture.Width, (float)scytheTexture.Height) : new Vector2(0f, (float)scytheTexture.Height));
		float scytheRotation = base.Projectile.rotation;
		Main.EntitySpriteDraw(scytheTexture, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), scytheRotation, handleOrigin, base.Projectile.scale, (SpriteEffects)(base.Projectile.ai[2] == -1f));
		return false;
	}
}
