CREATE TABLE `user` (
  `id` integer PRIMARY KEY,
  `username` varchar(255),
  `email` email,
  `phone_number` phone,
  `password` varchar(255),
  `user_role` ENUM ('moderator', 'admin', 'user') NOT NULL DEFAULT 'user'
);

CREATE TABLE `topic` (
  `id` int PRIMARY KEY,
  `title` varchar(255),
  `createdate` datetime,
  `user_id` int,
  `category_id` int
);

CREATE TABLE `category` (
  `id` int PRIMARY KEY,
  `name` varchar(255),
  `description` varchar(255),
  `create_date` datetime,
  `user_id` int
);

CREATE TABLE `post` (
  `id` int PRIMARY KEY,
  `content` varchar(255),
  `create_date` datetime,
  `update_date` datetiem,
  `user_id` int,
  `topic_id` int,
  `is_active` bool
);

CREATE TABLE `rooms` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `name` varchar(255),
  `create_date` datetime
);

CREATE TABLE `messages` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `user_id` int,
  `room_id` int,
  `content` varchar(255)
);

CREATE TABLE `user_rooms` (
  `user_id` int,
  `room_id` int,
  `joined_at` timestamp DEFAULT 'now()'
);

ALTER TABLE `post` ADD FOREIGN KEY (`user_id`) REFERENCES `user` (`id`);

ALTER TABLE `topic` ADD FOREIGN KEY (`user_id`) REFERENCES `user` (`id`);

ALTER TABLE `category` ADD FOREIGN KEY (`user_id`) REFERENCES `user` (`id`);

ALTER TABLE `topic` ADD FOREIGN KEY (`category_id`) REFERENCES `category` (`id`);

ALTER TABLE `post` ADD FOREIGN KEY (`topic_id`) REFERENCES `topic` (`id`);

ALTER TABLE `messages` ADD FOREIGN KEY (`user_id`) REFERENCES `user` (`id`);

ALTER TABLE `messages` ADD FOREIGN KEY (`room_id`) REFERENCES `rooms` (`id`);

ALTER TABLE `user_rooms` ADD FOREIGN KEY (`user_id`) REFERENCES `user` (`id`);

ALTER TABLE `user_rooms` ADD FOREIGN KEY (`room_id`) REFERENCES `rooms` (`id`);
