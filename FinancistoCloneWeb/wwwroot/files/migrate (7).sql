CreateUsersTable: create table "users" ("id" bigserial primary key not null, "name" varchar(255) not null, "email" varchar(255) not null, "email_verified_at" timestamp(0) without time zone null, "password" varchar(255) not null, "remember_token" varchar(100) null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateUsersTable: alter table "users" add constraint "users_email_unique" unique ("email")
CreatePasswordResetsTable: create table "password_resets" ("email" varchar(255) not null, "token" varchar(255) not null, "created_at" timestamp(0) without time zone null)
CreatePasswordResetsTable: create index "password_resets_email_index" on "password_resets" ("email")
CreateOauthAuthCodesTable: create table "oauth_auth_codes" ("id" varchar(100) not null, "user_id" bigint not null, "client_id" integer not null, "scopes" text null, "revoked" boolean not null, "expires_at" timestamp(0) without time zone null)
CreateOauthAuthCodesTable: alter table "oauth_auth_codes" add primary key ("id")
CreateOauthAccessTokensTable: create table "oauth_access_tokens" ("id" varchar(100) not null, "user_id" bigint null, "client_id" integer not null, "name" varchar(255) null, "scopes" text null, "revoked" boolean not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null, "expires_at" timestamp(0) without time zone null)
CreateOauthAccessTokensTable: alter table "oauth_access_tokens" add primary key ("id")
CreateOauthAccessTokensTable: create index "oauth_access_tokens_user_id_index" on "oauth_access_tokens" ("user_id")
CreateOauthRefreshTokensTable: create table "oauth_refresh_tokens" ("id" varchar(100) not null, "access_token_id" varchar(100) not null, "revoked" boolean not null, "expires_at" timestamp(0) without time zone null)
CreateOauthRefreshTokensTable: alter table "oauth_refresh_tokens" add primary key ("id")
CreateOauthRefreshTokensTable: create index "oauth_refresh_tokens_access_token_id_index" on "oauth_refresh_tokens" ("access_token_id")
CreateOauthClientsTable: create table "oauth_clients" ("id" serial primary key not null, "user_id" bigint null, "name" varchar(255) not null, "secret" varchar(100) not null, "redirect" text not null, "personal_access_client" boolean not null, "password_client" boolean not null, "revoked" boolean not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateOauthClientsTable: create index "oauth_clients_user_id_index" on "oauth_clients" ("user_id")
CreateOauthPersonalAccessClientsTable: create table "oauth_personal_access_clients" ("id" serial primary key not null, "client_id" integer not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateOauthPersonalAccessClientsTable: create index "oauth_personal_access_clients_client_id_index" on "oauth_personal_access_clients" ("client_id")
CreatePermissionTables: create table "permissions" ("id" serial primary key not null, "name" varchar(255) not null, "guard_name" varchar(255) not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreatePermissionTables: create table "roles" ("id" serial primary key not null, "name" varchar(255) not null, "guard_name" varchar(255) not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreatePermissionTables: create table "model_has_permissions" ("permission_id" integer not null, "model_type" varchar(255) not null, "model_id" bigint not null)
CreatePermissionTables: create index "model_has_permissions_model_id_model_type_index" on "model_has_permissions" ("model_id", "model_type")
CreatePermissionTables: alter table "model_has_permissions" add constraint "model_has_permissions_permission_id_foreign" foreign key ("permission_id") references "permissions" ("id") on delete cascade
CreatePermissionTables: alter table "model_has_permissions" add primary key ("permission_id", "model_id", "model_type")
CreatePermissionTables: create table "model_has_roles" ("role_id" integer not null, "model_type" varchar(255) not null, "model_id" bigint not null)
CreatePermissionTables: create index "model_has_roles_model_id_model_type_index" on "model_has_roles" ("model_id", "model_type")
CreatePermissionTables: alter table "model_has_roles" add constraint "model_has_roles_role_id_foreign" foreign key ("role_id") references "roles" ("id") on delete cascade
CreatePermissionTables: alter table "model_has_roles" add primary key ("role_id", "model_id", "model_type")
CreatePermissionTables: create table "role_has_permissions" ("permission_id" integer not null, "role_id" integer not null)
CreatePermissionTables: alter table "role_has_permissions" add constraint "role_has_permissions_permission_id_foreign" foreign key ("permission_id") references "permissions" ("id") on delete cascade
CreatePermissionTables: alter table "role_has_permissions" add constraint "role_has_permissions_role_id_foreign" foreign key ("role_id") references "roles" ("id") on delete cascade
CreatePermissionTables: alter table "role_has_permissions" add primary key ("permission_id", "role_id")
CreateHotelsTable: create table "hotels" ("id" char(3) not null, "name" varchar(255) not null, "code" varchar(255) not null, "country_id" varchar(255) not null, "city_id" varchar(255) not null, "url_php5" varchar(255) not null, "url_php7" varchar(255) not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateHotelsTable: alter table "hotels" add primary key ("id")
CreateHotelsTable: comment on column "hotels"."code" is 'Sigla que identifica el hotel'
CreateRestaurantsTable: create table "restaurants" ("id" serial primary key not null, "name" varchar(255) not null, "description" varchar(255) not null, "image" varchar(255) null, "status" boolean not null, "location" varchar(255) not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null, "hotel_id" char(3) not null)
CreateRestaurantsTable: alter table "restaurants" add constraint "restaurants_hotel_id_foreign" foreign key ("hotel_id") references "hotels" ("id")
CreateRestaurantsTable: comment on column "restaurants"."image" is 'url de imagen para el restaurante'
CreateRestaurantsTable: comment on column "restaurants"."location" is 'ubicación física del restaurante'
CreateDishTypesTable: create table "dish_types" ("id" bigserial primary key not null, "name" varchar(255) not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateDishTypesTable: create index "dish_types_name_index" on "dish_types" ("name")
CreateDishesTable: create table "dishes" ("id" bigserial primary key not null, "name" varchar(255) not null, "description" text null, "accompanying" varchar(255) null, "image" varchar(255) null, "type_id" bigint not null, "is_active" boolean not null default '1', "translations" jsonb not null default '1', "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateDishesTable: alter table "dishes" add constraint "dishes_type_id_foreign" foreign key ("type_id") references "dish_types" ("id") on delete restrict on update restrict
CreateDishesTable: create index "dishes_name_index" on "dishes" ("name")
CreateDishesTable: create index "dishes_description_index" on "dishes" ("description")
CreateDishesTable: create index "dishes_accompanying_index" on "dishes" ("accompanying")
CreateDishesTable: create index "dishes_image_index" on "dishes" ("image")
CreateDishesTable: create index "dishes_type_id_index" on "dishes" ("type_id")
CreateDishesTable: comment on column "dishes"."type_id" is '1 Entrada, 2 Plato fuerte, 3 postre, 4 bebida'
CreateMealShiftsTable: create table "meal_shifts" ("id" bigserial primary key not null, "name" varchar(255) not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateMealShiftsTable: create index "meal_shifts_name_index" on "meal_shifts" ("name")
CreateMealShiftsTable: comment on column "meal_shifts"."name" is '1=Desayuno 2=Almuerzo 3=Cena'
CreateMenuTypesTable: create table "menu_types" ("id" bigserial primary key not null, "name" varchar(255) not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateMenusTable: create table "menus" ("id" bigserial primary key not null, "name" varchar(255) not null, "description" varchar(255) not null, "meal_shift_id" bigint not null, "date" date not null, "type_id" bigint not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateMenusTable: alter table "menus" add constraint "menus_meal_shift_id_foreign" foreign key ("meal_shift_id") references "meal_shifts" ("id") on delete restrict on update restrict
CreateMenusTable: alter table "menus" add constraint "menus_type_id_foreign" foreign key ("type_id") references "menu_types" ("id") on delete restrict on update restrict
CreateMenusTable: create index "menus_meal_shift_id_index" on "menus" ("meal_shift_id")
CreateMenusTable: create index "menus_type_id_index" on "menus" ("type_id")
CreateHotelHasHotelsTable: create table "hotel_has_hotels" ("id" bigserial primary key not null, "hotel_id" integer not null, "hotel_related_id" integer not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateServiceTypesTable: create table "service_types" ("id" bigserial primary key not null, "description" varchar(255) not null, "status" char(1) not null default 'A', "opening_time" time(0) without time zone not null, "closing_time" time(0) without time zone not null, "future_day_flag" boolean not null default '1', "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateServiceTypesTable: create index "service_types_description_index" on "service_types" ("description")
CreateServiceTypesTable: comment on column "service_types"."opening_time" is 'Hora de apertura'
CreateServiceTypesTable: comment on column "service_types"."closing_time" is 'Hora de cierre'
CreateServiceTypesTable: comment on column "service_types"."future_day_flag" is 'Mostrar este servicio al activarse el días siguiente para hacer reservas cuando se llegue al total de cupos o cuando se cierre el sitting'
CreateSittingsTable: create table "sittings" ("id" bigserial primary key not null, "restaurant_id" bigint not null, "date_start" date not null, "date_end" date not null, "time_start" time(0) without time zone not null, "time_end" time(0) without time zone not null, "is_active" boolean not null default '1', "sitting_type" varchar(255) null, "service_type_id" bigint not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateSittingsTable: alter table "sittings" add constraint "sittings_restaurant_id_foreign" foreign key ("restaurant_id") references "restaurants" ("id") on delete restrict on update restrict
CreateSittingsTable: alter table "sittings" add constraint "sittings_service_type_id_foreign" foreign key ("service_type_id") references "service_types" ("id") on delete restrict on update restrict
CreateSittingsTable: create index "sittings_restaurant_id_index" on "sittings" ("restaurant_id")
CreateSittingsTable: create index "sittings_date_start_index" on "sittings" ("date_start")
CreateSittingsTable: create index "sittings_date_end_index" on "sittings" ("date_end")
CreateSittingsTable: create index "sittings_time_start_index" on "sittings" ("time_start")
CreateSittingsTable: create index "sittings_time_end_index" on "sittings" ("time_end")
CreateSittingsTable: create index "sittings_service_type_id_index" on "sittings" ("service_type_id")
CreateSittingsTable: comment on column "sittings"."date_start" is 'Fecha de inicio del turno'
CreateSittingsTable: comment on column "sittings"."date_end" is 'Fecha de fin del turno'
CreateSittingsTable: comment on column "sittings"."time_start" is 'Hora inicio del turno'
CreateSittingsTable: comment on column "sittings"."time_end" is 'Hora fin del turno'
CreateSittingDistributionsTable: create table "sitting_distributions" ("id" bigserial primary key not null, "sitting_id" bigint not null, "restaurant_id" bigint not null, "available" integer not null, "used" integer not null, "bloqued" integer not null, "is_buffet" integer not null default '0', "is_plus" integer not null default '0', "date" date not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateSittingDistributionsTable: alter table "sitting_distributions" add constraint "sitting_distributions_sitting_id_foreign" foreign key ("sitting_id") references "sittings" ("id") on delete restrict on update restrict
CreateSittingDistributionsTable: alter table "sitting_distributions" add constraint "sitting_distributions_restaurant_id_foreign" foreign key ("restaurant_id") references "restaurants" ("id") on delete restrict on update restrict
CreateSittingDistributionsTable: create index "sitting_distributions_sitting_id_index" on "sitting_distributions" ("sitting_id")
CreateSittingDistributionsTable: create index "sitting_distributions_restaurant_id_index" on "sitting_distributions" ("restaurant_id")
CreateSittingDistributionsTable: comment on column "sitting_distributions"."available" is 'Capacidad o numero de personas que puede reservar ese sitting'
CreateSittingDistributionsTable: comment on column "sitting_distributions"."used" is 'Se aumenta con un trigger; cada vez que se afecta el cupo y el estado se suma'
CreateSittingDistributionsTable: comment on column "sitting_distributions"."date" is 'Corresponde a la fecha del sitting'
CreateDishDistributionsTable: create table "dish_distributions" ("id" bigserial primary key not null, "sitting_distribution_id" bigint not null, "dish_id" bigint not null, "available" integer not null default '0', "ordered" integer not null default '0', "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateDishDistributionsTable: alter table "dish_distributions" add constraint "dish_distributions_sitting_distribution_id_foreign" foreign key ("sitting_distribution_id") references "sitting_distributions" ("id") on delete restrict on update restrict
CreateDishDistributionsTable: alter table "dish_distributions" add constraint "dish_distributions_dish_id_foreign" foreign key ("dish_id") references "dishes" ("id") on delete restrict on update restrict
CreateDishDistributionsTable: create index "dish_distributions_sitting_distribution_id_index" on "dish_distributions" ("sitting_distribution_id")
CreateDishDistributionsTable: create index "dish_distributions_dish_id_index" on "dish_distributions" ("dish_id")
CreateDishDistributionsTable: comment on column "dish_distributions"."available" is 'Cantidad de platos disponibles para la distribución del sitting'
CreateDishDistributionsTable: comment on column "dish_distributions"."ordered" is 'Cantidad de platos ordenados'
CreateDishMenuTable: create table "dish_menu" ("id" bigserial primary key not null, "menu_id" bigint not null, "dish_id" bigint not null)
CreateDishMenuTable: alter table "dish_menu" add constraint "dish_menu_menu_id_foreign" foreign key ("menu_id") references "menus" ("id") on delete restrict on update restrict
CreateDishMenuTable: alter table "dish_menu" add constraint "dish_menu_dish_id_foreign" foreign key ("dish_id") references "dishes" ("id") on delete restrict on update restrict
CreateDishMenuTable: create index "dish_menu_menu_id_index" on "dish_menu" ("menu_id")
CreateDishMenuTable: create index "dish_menu_dish_id_index" on "dish_menu" ("dish_id")
CreateBookingsTable: create table "bookings" ("id" bigserial primary key not null, "restaurant_id" bigint not null, "room" varchar(255) null, "name" varchar(255) null, "quotas" integer null, "sitting_distribution_id" bigint null, "user" varchar(255) null, "status" varchar(255) null, "bracelet_number" integer null, "number" integer null, "allergy" text null, "observations" text null, "assistance" varchar(255) null, "assistance_quota" integer null, "assignment" varchar(255) null, "email" varchar(255) null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateBookingsTable: alter table "bookings" add constraint "bookings_restaurant_id_foreign" foreign key ("restaurant_id") references "restaurants" ("id") on delete restrict on update restrict
CreateBookingsTable: alter table "bookings" add constraint "bookings_sitting_distribution_id_foreign" foreign key ("sitting_distribution_id") references "sitting_distributions" ("id") on delete restrict on update restrict
CreateBookingsTable: create index "bookings_restaurant_id_index" on "bookings" ("restaurant_id")
CreateBookingsTable: create index "bookings_room_index" on "bookings" ("room")
CreateBookingsTable: create index "bookings_name_index" on "bookings" ("name")
CreateBookingsTable: create index "bookings_sitting_distribution_id_index" on "bookings" ("sitting_distribution_id")
CreateBookingsTable: create index "bookings_user_index" on "bookings" ("user")
CreateBookingsTable: create index "bookings_status_index" on "bookings" ("status")
CreateBookingsTable: create index "bookings_bracelet_number_index" on "bookings" ("bracelet_number")
CreateBookingsTable: create index "bookings_number_index" on "bookings" ("number")
CreateBookingsTable: comment on column "bookings"."room" is 'Número de habitación'
CreateBookingsTable: comment on column "bookings"."name" is 'Nombre reserva'
CreateBookingsTable: comment on column "bookings"."quotas" is 'Cupos de reserva'
CreateOrdersTable: create table "orders" ("id" bigserial primary key not null, "booking_id" bigint not null, "restaurant_id" bigint not null, "dish_id" bigint not null, "sitting_distribution_id" bigint not null, "hotel_id" char(3) not null, "quantity_dishes" bigint not null default '0', "status" varchar(255) not null, "created_at" timestamp(0) without time zone null, "updated_at" timestamp(0) without time zone null)
CreateOrdersTable: alter table "orders" add constraint "orders_restaurant_id_foreign" foreign key ("restaurant_id") references "restaurants" ("id") on delete restrict on update restrict
CreateOrdersTable: alter table "orders" add constraint "orders_dish_id_foreign" foreign key ("dish_id") references "dishes" ("id") on delete restrict on update restrict
CreateOrdersTable: alter table "orders" add constraint "orders_sitting_distribution_id_foreign" foreign key ("sitting_distribution_id") references "sitting_distributions" ("id") on delete restrict on update restrict
CreateOrdersTable: alter table "orders" add constraint "orders_hotel_id_foreign" foreign key ("hotel_id") references "hotels" ("id") on delete restrict on update restrict
CreateOrdersTable: create index "orders_booking_id_index" on "orders" ("booking_id")
CreateOrdersTable: create index "orders_restaurant_id_index" on "orders" ("restaurant_id")
CreateOrdersTable: create index "orders_dish_id_index" on "orders" ("dish_id")
CreateOrdersTable: create index "orders_sitting_distribution_id_index" on "orders" ("sitting_distribution_id")
CreateOrdersTable: create index "orders_hotel_id_index" on "orders" ("hotel_id")
CreateOrdersTable: create index "orders_quantity_dishes_index" on "orders" ("quantity_dishes")
AddMenuIdColumnDishDistributionsTable: alter table "dish_distributions" add column "menu_id" bigint not null
AddMenuIdColumnDishDistributionsTable: alter table "dish_distributions" add constraint "dish_distributions_menu_id_foreign" foreign key ("menu_id") references "menus" ("id") on delete restrict on update restrict
AddMenuIdColumnDishDistributionsTable: create index "dish_distributions_menu_id_index" on "dish_distributions" ("menu_id")
AddOrderToDishTypesTable: alter table "dish_types" add column "order" integer null
AddOrderToDishTypesTable: alter table "dish_types" add constraint "dish_types_order_unique" unique ("order")
AddIsActiveColumnToMenusTable: alter table "menus" add column "is_active" boolean not null default '1'
AddDeletedAtColumnToMenusTable: alter table "menus" add column "deleted_at" timestamp(0) without time zone null
