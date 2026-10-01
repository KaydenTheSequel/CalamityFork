using CalamityMod.Cooldowns;
using CalamityMod.DataStructures;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class SandCloakVeil : ModProjectile, ILocalizedModType, IModType
{
	private const float Radius = 360f;

	private const int Duration = 900;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 450;
		base.Projectile.height = 450;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 900;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.scale = 1.5f;
	}

	public override void AI()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += 0.025f;
		Player Owner = Main.player[base.Projectile.owner];
		Player BuffedPlayer = Main.LocalPlayer;
		Vector2 posDiff = BuffedPlayer.Center - base.Projectile.Center;
		if (((Vector2)(ref posDiff)).Length() <= 360f)
		{
			BuffedPlayer.Calamity().getSandCloakAccelBoost = true;
			BuffedPlayer.statDefense += global::CalamityMod.Items.Accessories.SandCloak.SandVeilDefenseBoost;
		}
		else
		{
			BuffedPlayer.Calamity().getSandCloakAccelBoost = false;
		}
		if (base.Projectile.timeLeft == 1)
		{
			BuffedPlayer.Calamity().getSandCloakAccelBoost = false;
		}
		float ownerDist = Vector2.Distance(base.Projectile.Center, Owner.Center);
		if (ownerDist > 72f)
		{
			Projectile projectile = base.Projectile;
			projectile.Center += Vector2.Normalize(Owner.Center - base.Projectile.Center) * ((ownerDist > 180f) ? 2.5f : 1.25f);
		}
		if (Owner.dashDelay == -1 && base.Projectile.timeLeft < 855 && base.Projectile.timeLeft > 25)
		{
			base.Projectile.timeLeft = 25;
		}
		Circle dustCircle = new Circle(base.Projectile.Center, 360f);
		for (int i = 0; i < 2; i++)
		{
			Vector2 dustPos = dustCircle.RandomPointInCircle();
			Vector2 center = dustPos - base.Projectile.Center;
			if (((Vector2)(ref center)).Length() > 48f)
			{
				Vector2 spinningpoint = base.Projectile.SafeDirectionTo(dustPos);
				center = default(Vector2);
				Vector2 dustVel = spinningpoint.RotatedBy(-0.7853981852531433, center) * Vector2.Distance(base.Projectile.Center, dustPos) * 0.04f;
				Dust dust = Dust.NewDustPerfect(dustPos, 32, dustVel, 0, default(Color), 0.5f);
				dust.noGravity = true;
				dust.fadeIn = 1f;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		Main.player[base.Projectile.owner].AddCooldown(global::CalamityMod.Cooldowns.SandCloak.ID, CalamityUtils.SecondsToFrames(15));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		float scaleStep = 0.05f;
		float rotationOffset = 0.03f;
		Color drawCol = base.Projectile.GetAlpha(Color.Lerp(lightColor, Color.White, 0.5f));
		float drawTransparency = 0.1f;
		if (base.Projectile.timeLeft > 890)
		{
			drawTransparency = (float)(900 - base.Projectile.timeLeft) * 0.01f;
		}
		else if (base.Projectile.timeLeft < 25)
		{
			drawTransparency = (float)base.Projectile.timeLeft * 0.004f;
		}
		for (int i = 0; i < 20; i++)
		{
			float rotation = (base.Projectile.rotation + rotationOffset * (float)i * (float)i) * (float)(i % 2 == 0).ToDirectionInt();
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, drawCol * drawTransparency, rotation, tex.Size() / 2f, 1.584f - (float)i * scaleStep, (SpriteEffects)0);
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (target.Center.X > base.Projectile.Center.X).ToDirectionInt();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 360f, targetHitbox);
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
