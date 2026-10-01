using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class OldDukeVortex : ModProjectile, ILocalizedModType, IModType
{
	private Vector2 cen;

	public static SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeVortex");

	public SlotId SoundId;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		SpawnSound.MaxInstances = 50;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SpawnSound with
		{
			IsLooped = true,
			MaxInstances = 20
		};
		SoundId = SoundEngine.PlaySound(in style, base.Projectile.Center, (ActiveSound _) => new ProjectileAudioTracker(base.Projectile).IsActiveAndInGame());
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 408;
		base.Projectile.height = 408;
		base.Projectile.scale = 0.004f;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 0;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 1800;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[1] == 0f)
		{
			cen = base.Projectile.Center;
			base.Projectile.localAI[1] = 1f;
		}
		base.Projectile.position = cen - new Vector2((float)(base.Projectile.width / 2)) * base.Projectile.scale;
		if (Main.zenithWorld)
		{
			if (base.Projectile.scale < 2f)
			{
				if (base.Projectile.alpha > 0)
				{
					base.Projectile.alpha--;
				}
				base.Projectile.scale += 0.004f;
				if (base.Projectile.scale > 2f)
				{
					base.Projectile.scale = 2f;
				}
			}
			else if (base.Projectile.timeLeft <= 85)
			{
				if (base.Projectile.alpha < 255)
				{
					base.Projectile.alpha += 3;
				}
				base.Projectile.scale -= 0.012f;
			}
		}
		else if (base.Projectile.scale < 1f)
		{
			if (base.Projectile.alpha > 0)
			{
				base.Projectile.alpha--;
			}
			base.Projectile.scale += 0.004f;
			if (base.Projectile.scale > 1f)
			{
				base.Projectile.scale = 1f;
			}
			base.Projectile.width = (base.Projectile.height = (int)(408f * base.Projectile.scale));
		}
		else if (base.Projectile.timeLeft <= 85)
		{
			if (base.Projectile.alpha < 255)
			{
				base.Projectile.alpha += 3;
			}
			base.Projectile.scale -= 0.012f;
			base.Projectile.width = (base.Projectile.height = (int)(408f * base.Projectile.scale));
		}
		else
		{
			base.Projectile.width = (base.Projectile.height = 408);
		}
		float distanceRequired = 800f * base.Projectile.scale;
		float succPower = (Main.zenithWorld ? 1f : 0.5f);
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			float distance = Vector2.Distance(player.Center, cen);
			if (distance < distanceRequired && player.grappling[0] == -1 && Collision.CanHit(cen, 1, 1, player.Center, 1, 1))
			{
				float distanceRatio = distance / distanceRequired;
				float wingTimeSet = (float)Math.Ceiling((float)player.wingTimeMax * 0.5f * distanceRatio);
				if (player.wingTime > wingTimeSet)
				{
					player.wingTime = wingTimeSet;
				}
				float multiplier = 1f - distanceRatio;
				if (player.Center.X < cen.X)
				{
					player.velocity.X += succPower * multiplier;
				}
				else
				{
					player.velocity.X -= succPower * multiplier;
				}
			}
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] % 10f == 1f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(cen, Vector2.Zero, new Color(55, 195, 0, 20), "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), base.Projectile.scale * 0.9f, base.Projectile.scale * 0.4f, 40, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		if (base.Projectile.timeLeft <= 85)
		{
			base.Projectile.localAI[2] += 1f / 85f;
		}
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.rotation -= 0.1f * (float)(1.0 - (double)base.Projectile.alpha / 255.0);
		float lightAmt = 2f * base.Projectile.scale;
		Lighting.AddLight(cen, lightAmt, lightAmt * 2f, lightAmt);
		float maxdist = 1200f;
		if (SoundEngine.TryGetActiveSound(SoundId, out ActiveSound Sound) && Sound.IsPlaying)
		{
			Sound.Position = cen;
			Sound.Volume = base.Projectile.scale;
			Sound.Pitch = MathHelper.Lerp(0f, -1f, MathHelper.Clamp((base.Projectile.Distance(Main.LocalPlayer.Center) - 800f) / maxdist, 0f, 1f) + (0f - base.Projectile.scale + 1f));
		}
		if (base.Projectile.timeLeft > 85)
		{
			Vector2 vec2 = cen + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(320f, 540f) * base.Projectile.scale, 0f), 6.2831854820251465);
			GeneralParticleHandler.SpawnParticle(new SparkParticle(vec2, (cen - vec2) / 20f, affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1f), Color.LimeGreen, fadeIn: true));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> Tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		float sc = MathHelper.Lerp(1f, 0f, base.Projectile.localAI[2]);
		float alphaLerp = MathHelper.Lerp(1f, 0f, (float)base.Projectile.alpha / 255f);
		Main.EntitySpriteDraw(Tex.Value, cen - Main.screenPosition, Tex.Frame(), Utils.MultiplyRGBA(new Color(0f, 0f, 0f, 0.4f), new Color(alphaLerp, alphaLerp, alphaLerp, alphaLerp)), (0f - base.Projectile.rotation) / 2f * 5f, Tex.Frame().Center(), 1.61f * base.Projectile.scale * sc, (SpriteEffects)0);
		for (int i = 2; i >= 0; i--)
		{
			float lerp = (float)i / 3f;
			Main.EntitySpriteDraw(Tex.Value, cen - Main.screenPosition, Tex.Frame(), Color.Lerp(new Color(5, 155, 95, 100), new Color(255, 255, 255, 55), lerp).MultiplyRGBA(new Color(alphaLerp, alphaLerp, alphaLerp, alphaLerp)), (0f - base.Projectile.rotation) / 2f * (float)(i + 1), Tex.Frame().Center(), MathHelper.Lerp(1f, 1.7f, lerp) * base.Projectile.scale * sc, (SpriteEffects)0);
		}
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		if (base.Projectile.timeLeft <= 1680)
		{
			return base.Projectile.timeLeft > 85;
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 210f * base.Projectile.scale, targetHitbox);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.timeLeft <= 1680 && base.Projectile.timeLeft > 85)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 600);
		}
	}

	public OldDukeVortex()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		cen = Vector2.Zero;
		base._002Ector();
	}
}
