using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Sacrifice : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.65f;

	public override float StealthVelocityMultiplier => 1.5f;

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 68);
		base.Item.damage = 300;
		base.Item.useAnimation = (base.Item.useTime = 9);
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SacrificeProjectile>();
		base.Item.shootSpeed = 16f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
	}

	public override bool AltFunctionUse(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] > 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == type && p.owner == player.whoAmI && p.ai[0] == 1f)
				{
					NPC attachedNPC = Main.npc[(int)p.ai[1]];
					p.ai[0] = 2f;
					p.ModProjectile<SacrificeProjectile>().AbleToHealOwner = attachedNPC.type != 488 && attachedNPC.type != ModContent.NPCType<SuperDummyNPC>();
					p.netUpdate = true;
				}
			}
			return false;
		}
		position += velocity * 3f;
		int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (player.Calamity().StealthStrikeAvailable() && Main.projectile.IndexInRange(proj))
		{
			Main.projectile[proj].Calamity().stealthStrike = true;
		}
		return false;
	}
}
