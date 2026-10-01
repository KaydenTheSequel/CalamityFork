using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class WhitewaterProj : ModProjectile, ILocalizedModType, IModType
{
	public bool returning;

	public bool empowered;

	public bool shattered;

	public int shatterTimer = 230;

	public float fade;

	public float damageModifier = 1f;

	public float randSize;

	public int returnTime = 500;

	public int becomeEmpoweredTime = 60;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Whitewater";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 800;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		randSize = Main.rand.NextFloat(0.8f, 1.2f);
		base.Projectile.rotation += 0.02f * ((Vector2)(ref base.Projectile.velocity)).Length();
		if (time >= (float)returnTime)
		{
			returning = true;
		}
		if (time > (float)becomeEmpoweredTime && time < (float)returnTime && !shattered)
		{
			fade = MathHelper.Lerp(fade, 1f, 0.06f);
			empowered = true;
		}
		else
		{
			fade = MathHelper.Lerp(fade, 0f, 0.07f);
			empowered = false;
		}
		Color newColor;
		if (empowered)
		{
			Vector2 center = base.Projectile.Center;
			newColor = Color.LightBlue;
			Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * fade);
			if (Main.rand.NextBool(5))
			{
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, 278, null, 0, newColor);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.3f, 0.55f);
				dust.velocity = base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.4f);
				dust.color = Color.LightBlue;
			}
		}
		if (time > 20f && !returning && !shattered)
		{
			Vector2 moveToMouse = (Owner.ClampedMouseWorld() - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 12f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += moveToMouse * (1f - 0.85f * Utils.GetLerpValue(returnTime / 2, 0f, time, clamped: true));
			}
			else
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.9f;
			}
		}
		if (shattered && !returning)
		{
			fade = MathHelper.Lerp(fade, 1f, 0.07f);
			base.Projectile.extraUpdates = 5;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.alpha = 255;
			randSize = Main.rand.NextFloat(1.8f, 2.2f);
			base.Projectile.timeLeft++;
			time--;
			if (shatterTimer <= 140 && Main.rand.NextBool(5))
			{
				Vector2 vel = (Vector2.One * 19f).RotatedByRandom(100.0) * Main.rand.NextFloat(0.9f, 1.1f);
				Vector2 position2 = base.Projectile.Center + vel * 3f;
				newColor = default(Color);
				Dust dust2 = Dust.NewDustPerfect(position2, 66, null, 0, newColor);
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.5f, 0.85f);
				dust2.velocity = -vel * Main.rand.NextFloat(0.2f, 0.4f);
				dust2.color = Color.LightBlue;
			}
			if (shatterTimer == 230)
			{
				for (int i = 0; i < 25; i++)
				{
					Vector2 vel2 = (Vector2.One * 24f).RotatedByRandom(100.0) * Main.rand.NextFloat(0.9f, 1.1f);
					Vector2 center2 = base.Projectile.Center;
					newColor = default(Color);
					Dust dust3 = Dust.NewDustPerfect(center2, 66, null, 0, newColor);
					dust3.noGravity = true;
					dust3.scale = Main.rand.NextFloat(0.5f, 0.85f);
					dust3.velocity = vel2 * Main.rand.NextFloat(0.2f, 0.4f);
					dust3.color = Color.LightBlue;
				}
			}
			shatterTimer--;
			if (shatterTimer <= 0)
			{
				base.Projectile.alpha = 0;
				empowered = false;
				returning = true;
			}
		}
		if (returning)
		{
			base.Projectile.extraUpdates = 1;
			base.Projectile.alpha = 0;
			float acceleration = 1.2f;
			Vector2 center3 = Owner.Center;
			float xDist = center3.X - base.Projectile.Center.X;
			float yDist = center3.Y - base.Projectile.Center.Y;
			float dist = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
			dist = 13f / dist;
			xDist *= dist;
			yDist *= dist;
			if (base.Projectile.velocity.X < xDist)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + acceleration;
				if (base.Projectile.velocity.X < 0f && xDist > 0f)
				{
					base.Projectile.velocity.X += acceleration;
				}
			}
			else if (base.Projectile.velocity.X > xDist)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - acceleration;
				if (base.Projectile.velocity.X > 0f && xDist < 0f)
				{
					base.Projectile.velocity.X -= acceleration;
				}
			}
			if (base.Projectile.velocity.Y < yDist)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + acceleration;
				if (base.Projectile.velocity.Y < 0f && yDist > 0f)
				{
					base.Projectile.velocity.Y += acceleration;
				}
			}
			else if (base.Projectile.velocity.Y > yDist)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - acceleration;
				if (base.Projectile.velocity.Y > 0f && yDist < 0f)
				{
					base.Projectile.velocity.Y -= acceleration;
				}
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Rectangle hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(Owner.Hitbox))
				{
					if (base.Projectile.Calamity().stealthStrike)
					{
						ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
						while (enumerator.MoveNext())
						{
							Projectile p = enumerator.Current;
							if (p.type == ModContent.ProjectileType<WhitewaterAura>() && p.owner == base.Projectile.owner && p.timeLeft > 30)
							{
								p.timeLeft = 30;
							}
						}
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<WhitewaterAura>(), (int)((float)base.Projectile.damage * 0.25f), base.Projectile.knockBack, base.Projectile.owner);
					}
					base.Projectile.Kill();
				}
			}
		}
		time++;
	}

	public override bool? CanDamage()
	{
		if (!shattered && !returning)
		{
			return null;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		if (damageModifier > 0.05f)
		{
			damageModifier -= 0.05f;
		}
		if (empowered && !shattered)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/BreakAndReform");
			style.Volume = 0.4f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			int points = (base.Projectile.Calamity().stealthStrike ? 6 : 4);
			float radians = (float)Math.PI * 2f / (float)points;
			Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
			for (int k = 0; k < points; k++)
			{
				Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy((base.Projectile.ai[1] == 1f) ? MathHelper.ToRadians(45f) : 0f) * 4f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, -velocity, ModContent.ProjectileType<WhitewaterSpear>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner);
			}
			shattered = true;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.Calamity().stealthStrike)
		{
			modifiers.SourceDamage *= damageModifier;
		}
		else
		{
			modifiers.SourceDamage *= ((empowered && !returning) ? damageModifier : (damageModifier * 0.5f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Texture2D rTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D wTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2).Value;
		Color drawColor2 = Color.LightBlue;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Color val = drawColor2;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rTexture, position, null, val * fade * 0.5f, base.Projectile.rotation, rTexture.Size() * 0.5f, 0.45f * randSize, (SpriteEffects)0);
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		for (int k = 0; k < 2; k++)
		{
			float rot = ((k == 0) ? MathHelper.ToRadians(90f) : 0f) + ((base.Projectile.ai[1] == -1f) ? MathHelper.ToRadians(45f) : 0f);
			Vector2 position2 = base.Projectile.Center - Main.screenPosition;
			val = drawColor2;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(wTexture, position2, null, val * fade * 0.4f, rot, wTexture.Size() * 0.5f, 1.15f * randSize, (SpriteEffects)0);
			Vector2 position3 = base.Projectile.Center - Main.screenPosition;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(wTexture, position3, null, val * fade * 0.4f, rot, wTexture.Size() * 0.5f, 0.68f * randSize, (SpriteEffects)0);
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 45f, targetHitbox);
	}
}
