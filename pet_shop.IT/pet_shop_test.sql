-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Jan 15, 2023 at 05:21 PM
-- Server version: 5.7.24
-- PHP Version: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pet_shop_test`
--

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `order_number` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `pet_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`order_number`, `user_id`, `pet_id`) VALUES
(14, 14, 5),
(17, 14, 8),
(18, 14, 4),
(19, 14, 10),
(20, 13, 3),
(21, 13, 11),
(23, 1, 15),
(24, 1, 20);

-- --------------------------------------------------------

--
-- Table structure for table `pets`
--

CREATE TABLE `pets` (
  `pet_id` int(11) NOT NULL,
  `shop_id` int(11) NOT NULL,
  `price` int(11) NOT NULL,
  `available` enum('yes','no') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `pets`
--

INSERT INTO `pets` (`pet_id`, `shop_id`, `price`, `available`) VALUES
(1, 2, 30000, 'yes'),
(2, 14, 2500, 'yes'),
(3, 14, 300000, 'no'),
(4, 6, 150000, 'no');

-- --------------------------------------------------------

--
-- Table structure for table `pet_info`
--

CREATE TABLE `pet_info` (
  `pet_id` int(10) UNSIGNED NOT NULL,
  `pet_type` enum('Cat','Dog','Hedgehog','Raccoon','Fox') NOT NULL,
  `name` varchar(100) NOT NULL,
  `age` int(11) NOT NULL,
  `color` varchar(100) NOT NULL,
  `can_swim` bit(1) NOT NULL,
  `reproduce_ability` bit(1) NOT NULL,
  `gender` enum('Male','Female') NOT NULL,
  `pet_breed` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `pet_info`
--

INSERT INTO `pet_info` (`pet_id`, `pet_type`, `name`, `age`, `color`, `can_swim`, `reproduce_ability`, `gender`, `pet_breed`) VALUES
(1, 'Cat', 'Edward', 6, 'Black', b'1', b'0', 'Male', 'Abyssinian Cat'),
(2, 'Fox', 'Zorg', 2, 'Red', b'1', b'1', 'Female', 'Derlansky'),
(3, 'Dog', 'noname', 1, 'gold', b'0', b'1', 'Male', 'Maltipoo'),
(4, 'Cat', 'Melissa', 1, 'Grey', b'0', b'0', 'Female', 'bob tail');

-- --------------------------------------------------------

--
-- Table structure for table `shops`
--

CREATE TABLE `shops` (
  `Shop_id` int(11) NOT NULL,
  `adress` varchar(100) NOT NULL,
  `city` varchar(100) NOT NULL,
  `owner` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `shops`
--

INSERT INTO `shops` (`Shop_id`, `adress`, `city`, `owner`) VALUES
(1, '6275_Misty_Pines', 'Москва', 'Сорокин Всеволод'),
(2, '2690_Round_Elk_Ledge', 'Гурзуф', 'Чернова Вероника'),
(3, '7091_Heather_Cider_Alley', 'Тула', 'Устинов Максим'),
(4, '9817_Sunny_By-pass', 'Тверь', 'Ефремов Виктор');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `login` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `name` varchar(100) NOT NULL,
  `surname` varchar(100) NOT NULL,
  `role` enum('admin','vendor','customer') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `login`, `password`, `name`, `surname`, `role`) VALUES
(1, 'admin', '1234', 'admin', 'admin', 'admin'),
(2, 'Danon', '1234', 'Dan', 'Nech', 'customer'),
(8, 'Max_Bobrov', '1234', 'Max', 'Bobrov', 'vendor'),
(9, 'NTrosina', '1234', 'Nastya', 'Trosina', 'customer'),
(10, 'BB32', '1234', 'Benjamin', 'Benier', 'customer');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`order_number`),
  ADD UNIQUE KEY `pet_id` (`pet_id`);

--
-- Indexes for table `pets`
--
ALTER TABLE `pets`
  ADD PRIMARY KEY (`pet_id`);

--
-- Indexes for table `pet_info`
--
ALTER TABLE `pet_info`
  ADD UNIQUE KEY `pet_id` (`pet_id`);

--
-- Indexes for table `shops`
--
ALTER TABLE `shops`
  ADD PRIMARY KEY (`Shop_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `login` (`login`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `order_number` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `pets`
--
ALTER TABLE `pets`
  MODIFY `pet_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `pet_info`
--
ALTER TABLE `pet_info`
  MODIFY `pet_id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `shops`
--
ALTER TABLE `shops`
  MODIFY `Shop_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
