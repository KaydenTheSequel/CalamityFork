using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MarksmanShot : ModProjectile, ILocalizedModType, IModType
{
	private static readonly int trailLength = 35;

	internal List<Vector2> trailPositions;

	internal static readonly int UpdateCount = 8;

	internal static readonly int Lifetime = UpdateCount * CalamityUtils.SecondsToFrames(2);

	public static readonly int RicochetCap = 999;

	internal static int RicochetPause = UpdateCount * 3;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	internal ref float NumRicochets => ref base.Projectile.ai[0];

	internal ref float RicochetFreezeTimer => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 4000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.MaxUpdates = UpdateCount;
		base.Projectile.alpha = 255;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.numHits != 0)
		{
			return false;
		}
		return null;
	}

	public override bool ShouldUpdatePosition()
	{
		return RicochetFreezeTimer <= 0f;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color val = Color.Gold * 0.8f;
		Lighting.AddLight(center, ((Color)(ref val)).ToVector3() * 0.5f);
		if (ShouldUpdatePosition())
		{
			if (trailPositions == null)
			{
				trailPositions = new List<Vector2>(trailLength);
				for (int i = 0; i < trailLength; i++)
				{
					trailPositions.Add(base.Projectile.Center);
				}
			}
			trailPositions.Insert(0, base.Projectile.Center);
			while (trailPositions.Count > trailLength)
			{
				trailPositions.RemoveAt(trailPositions.Count - 1);
			}
		}
		if (RicochetFreezeTimer > 0f)
		{
			RicochetFreezeTimer--;
		}
		else
		{
			if (!(NumRicochets < (float)RicochetCap))
			{
				return;
			}
			Projectile[] validCoins = base.Projectile.GetAvailableCoins();
			Projectile struckCoin = null;
			foreach (Projectile coin in validCoins)
			{
				if (Collision.CheckAABBvAABBCollision(coin.Hitbox.TopLeft(), coin.Hitbox.Size(), base.Projectile.Hitbox.TopLeft(), base.Projectile.Hitbox.Size()))
				{
					struckCoin = coin;
					break;
				}
			}
			if (struckCoin == null)
			{
				return;
			}
			Projectile[] otherCoins = validCoins.Where((Projectile proj) => proj.whoAmI != struckCoin.whoAmI).ToArray();
			RicoshotTarget nextCoin = base.Projectile.FindRicochetTarget(base.Projectile.Center, otherCoins, considerFrozenCoins: true);
			RicochetOffCoin(nextCoin, struckCoin);
			if (nextCoin.type == RicoshotTargetType.Coin)
			{
				Projectile nextCoinProj = Main.projectile[nextCoin.entityID];
				if (nextCoinProj.type == ModContent.ProjectileType<M1GarandEmptyClip>())
				{
					nextCoinProj.ai[1] = M1GarandEmptyClip.RicochetPause;
				}
				else
				{
					nextCoinProj.ai[1] = RicoshotCoin.RicochetPause;
				}
			}
		}
	}

	public void RicochetOffCoin(RicoshotTarget target, Projectile struckCoin)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		float speed = ((Vector2)(ref base.Projectile.velocity)).Length();
		base.Projectile.velocity = base.Projectile.DirectionTo(target.pos) * speed;
		CalamityGlobalProjectile cgp = base.Projectile.Calamity();
		if (NumRicochets == 0f && CalamityUtils.CanRicoshotCoinForceCrit(struckCoin))
		{
			cgp.forcedCrit = true;
		}
		float bonusDamageRatio = 0f;
		if (struckCoin.type == ModContent.ProjectileType<RicoshotCoin>())
		{
			float num = struckCoin.ai[0];
			float num2 = ((num == 1f) ? ((NumRicochets > 0f) ? RicoshotCoin.SilverMulticoinBonus : RicoshotCoin.SilverBonus) : ((num != 2f) ? ((NumRicochets > 0f) ? RicoshotCoin.CopperMulticoinBonus : RicoshotCoin.CopperBonus) : ((NumRicochets > 0f) ? RicoshotCoin.GoldMulticoinBonus : RicoshotCoin.GoldBonus)));
			bonusDamageRatio = num2;
		}
		else if (struckCoin.type == ModContent.ProjectileType<M1GarandEmptyClip>())
		{
			bonusDamageRatio = ((NumRicochets > 0f) ? M1GarandEmptyClip.ClipMulticlipBonus : M1GarandEmptyClip.ClipBonus);
		}
		cgp.totalRicoshotDamageBonus += bonusDamageRatio;
		NumRicochets++;
		if (target.IsValid)
		{
			if (struckCoin.type == ModContent.ProjectileType<M1GarandEmptyClip>())
			{
				RicochetFreezeTimer = M1GarandEmptyClip.RicochetPause;
			}
			else
			{
				RicochetFreezeTimer = RicoshotCoin.RicochetPause;
			}
		}
		SoundEngine.PlaySound(in RicoshotCoin.BlingHitSound, struckCoin.Center);
		if (target.IsValid)
		{
			RicochetFreezeTimer = RicochetPause;
		}
		SoundEngine.PlaySound(in RicoshotCoin.BlingHitSound, struckCoin.Center);
		Main.player[base.Projectile.owner].SetScreenshake(5f);
		struckCoin.localAI[0] = NumRicochets;
		struckCoin.Kill();
		if (struckCoin.owner == Main.myPlayer)
		{
			Player coinOwner = Main.player[struckCoin.owner];
			if (coinOwner.name.ToLower() == "v1" || coinOwner.name.ToLower() == "v2" || coinOwner.name.ToLower() == "mirage")
			{
				ORDERSystem.JUDGEMENT();
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Vector2.Zero;
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		if (base.Projectile.numHits == 0)
		{
			base.Projectile.timeLeft = Math.Min(Lifetime - base.Projectile.timeLeft, trailLength);
		}
		base.Projectile.numHits++;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(72, 60);
		if (base.Projectile.numHits == 0)
		{
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.timeLeft = Math.Min(Lifetime - base.Projectile.timeLeft, trailLength);
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		float fadeOpacity = Math.Min((float)base.Projectile.timeLeft / (float)trailLength, 1f);
		return Color.PaleGoldenrod * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float width = Math.Min((float)base.Projectile.timeLeft / (float)trailLength, 1f);
		return (1f - completionRatio) * 6.4f * width;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (trailPositions == null)
		{
			return false;
		}
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BasicTrail", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(trailPositions, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), trailLength);
		return false;
	}
}
