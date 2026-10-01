using System;
using System.Runtime.CompilerServices;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Cooldowns;
using CalamityMod.Effects;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

[PierceResistException(false)]
public class ShortCircuitHook : ModProjectile, ILocalizedModType, IModType
{
	public enum TaserAIState
	{
		Firing,
		Electrocuting,
		ReelingBack
	}

	public static readonly SoundStyle Explode = new SoundStyle("CalamityMod/Sounds/Item/ElectricBurst")
	{
		Volume = 0.8f
	};

	public bool giveCooldown;

	public bool onSpawn;

	public const float ReelbackSpeed = 40f;

	public Color hookColor;

	[CompilerGenerated]
	private SlotId _003CHum_003Ek__BackingField;

	public new string LocalizationCategory => "Projectiles.Misc";

	public TaserAIState AIState
	{
		get
		{
			return (TaserAIState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
		}
	}

	public float Time
	{
		get
		{
			return base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public int ElectrocutionTarget
	{
		get
		{
			return (int)base.Projectile.localAI[0];
		}
		set
		{
			base.Projectile.localAI[0] = value;
		}
	}

	public SlotId Hum
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CHum_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CHum_003Ek__BackingField = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.extraUpdates = 8;
		base.Projectile.tileCollide = false;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.ArmorPenetration = 10;
	}

	public override void AI()
	{
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		if (onSpawn)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 30f, ArsenalEffects.ArsenalElectricDust);
				dust.scale = (Main.rand.NextBool(7) ? 1.5f : 0.9f);
				dust.noGravity = true;
				dust.fadeIn = 2f;
				dust.color = ArsenalEffects.ArsenalElectricColor;
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.9f, 2.8f);
			}
			onSpawn = false;
		}
		if (AIState != TaserAIState.Firing)
		{
			Time++;
		}
		Player player = Main.player[base.Projectile.owner];
		hookColor = Color.Lerp(hookColor, Color.SlateGray, 0.25f);
		float distanceFromPlayer = base.Projectile.Distance(player.Center);
		switch (AIState)
		{
		case TaserAIState.Firing:
			if (distanceFromPlayer > 800f || Time >= 90f)
			{
				GoToAIState(TaserAIState.ReelingBack);
			}
			break;
		case TaserAIState.Electrocuting:
		{
			if (distanceFromPlayer > 2000f)
			{
				GoToAIState(TaserAIState.ReelingBack);
			}
			if (SoundEngine.TryGetActiveSound(Hum, out ActiveSound hum3) && hum3.IsPlaying)
			{
				hum3.Position = player.Center;
				hum3.Pitch = MathHelper.Lerp(0f, 1f, Utils.GetLerpValue(0f, 150f, Time, clamped: true));
			}
			if (Time == 150f || !Main.npc[ElectrocutionTarget].active)
			{
				if (!Main.npc[ElectrocutionTarget].active)
				{
					giveCooldown = false;
				}
				SoundEngine.PlaySound(in Explode, base.Projectile.Center);
				base.Projectile.localNPCHitCooldown = 15;
				for (int j = 0; j < 25; j++)
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalElectricDust, Utils.RotatedByRandom(new Vector2(9f, 9f), 100.0) * Main.rand.NextFloat(0.5f, 1f));
					dust2.scale = Main.rand.NextFloat(0.9f, 1.4f);
					dust2.noGravity = false;
					dust2.color = ArsenalEffects.ArsenalElectricColor;
					Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalElectricDust, Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.75f, 1f));
					dust3.scale = Main.rand.NextFloat(1.7f, 2.1f);
					dust3.noGravity = true;
					dust3.color = ArsenalEffects.ArsenalElectricColor;
				}
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalElectricColor * 0.5f, new Vector2(1f, 1f), 0f, 0.5f, 3f, 18));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 10, 1.3f, ArsenalEffects.ArsenalElectricColor, Vector2.One, useAddativeBlend: true, glowCenter: true));
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ShortCircuitExplosion>(), base.Projectile.damage * 8, 0f, base.Projectile.owner);
				Time = 0f;
				base.Projectile.velocity = base.Projectile.SafeDirectionTo(player.Center);
				GoToAIState(TaserAIState.ReelingBack);
				return;
			}
			if (Main.npc[ElectrocutionTarget].active)
			{
				base.Projectile.Center = Main.npc[ElectrocutionTarget].Center;
			}
			break;
		}
		case TaserAIState.ReelingBack:
		{
			if (SoundEngine.TryGetActiveSound(Hum, out ActiveSound hum2) && hum2.IsPlaying)
			{
				hum2?.Stop();
			}
			Rectangle hitbox = base.Projectile.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
			{
				if (giveCooldown)
				{
					player.Calamity().arsenalCooldown = 300;
					player.AddCooldown(ArsenalPower.ID, 300);
				}
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
				style.Volume = 0.5f;
				style.Pitch = 0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.Kill();
				return;
			}
			base.Projectile.tileCollide = false;
			base.Projectile.extraUpdates = 8;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(player.Center) * (40f / (float)base.Projectile.extraUpdates), 0.02f);
			break;
		}
		}
		base.Projectile.rotation = base.Projectile.AngleFrom(player.Center);
		ManipulatePlayerItemValues(player);
	}

	public void ManipulatePlayerItemValues(Player player)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir((player.Center.X - base.Projectile.Center.X < 0f).ToDirectionInt());
		player.itemRotation = CalamityUtils.WrapAngle90Degrees(base.Projectile.rotation);
		player.itemTime = 4;
		player.itemAnimation = 4;
	}

	public void GoToAIState(TaserAIState newAIState)
	{
		if (AIState != newAIState)
		{
			base.Projectile.penetrate = -1;
			AIState = newAIState;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		Texture2D texture = TextureAssets.Projectile[base.Projectile.type].Value;
		Texture2D lineTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineSoftEdge", (AssetRequestMode)2).Value;
		Vector2 altPos = obj.Center - Vector2.UnitY * 3f;
		float distance = altPos.Distance(base.Projectile.Center);
		int drawSeperation = 10;
		Vector2 toPoint = altPos.DirectionTo(base.Projectile.Center);
		for (int i = drawSeperation; (float)i < distance; i += drawSeperation)
		{
			Vector2 position = base.Projectile.Center - Main.screenPosition - toPoint * (float)i;
			Color color = hookColor;
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(lineTex, position, null, color, toPoint.ToRotation() + (float)Math.PI / 2f, lineTex.Size() * 0.5f, new Vector2(1f, 1.3f) * base.Projectile.scale * 0.01f, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.tileCollide = false;
		if (base.Projectile.localNPCHitCooldown > 7 && target == Main.npc[ElectrocutionTarget])
		{
			base.Projectile.localNPCHitCooldown--;
		}
		hookColor = Color.Cyan;
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 120);
		if (AIState == TaserAIState.Firing)
		{
			giveCooldown = true;
			base.Projectile.Center = target.Center;
			base.Projectile.extraUpdates = 1;
			if (!Main.dedServ)
			{
				for (int i = 0; i < 30; i++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalElectricDust, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.85f, 0.9f));
					dust.scale = Main.rand.NextFloat(0.9f, 1.2f);
					dust.noGravity = true;
					dust.color = ArsenalEffects.ArsenalElectricColor;
				}
			}
			ElectrocutionTarget = target.whoAmI;
			Time = 0f;
			SoundStyle charge = new SoundStyle("CalamityMod/Sounds/Item/LowHum");
			Hum = SoundEngine.PlaySound(charge with
			{
				Volume = 1.6f,
				IsLooped = true
			}, base.Projectile.Center);
			GoToAIState(TaserAIState.Electrocuting);
		}
		if (giveCooldown && target.life <= 0 && target.realLife == -1)
		{
			giveCooldown = false;
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.numHits != 0 && target != Main.npc[ElectrocutionTarget])
		{
			return false;
		}
		return null;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		GoToAIState(TaserAIState.ReelingBack);
		return false;
	}

	public ShortCircuitHook()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		onSpawn = true;
		hookColor = Color.SlateGray;
		base._002Ector();
	}
}
