-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 17, 2025 at 03:56 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.0.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pricenow`
--

-- --------------------------------------------------------

--
-- Table structure for table `loggings`
--

CREATE TABLE `loggings` (
  `ScrapeId` int(11) NOT NULL,
  `MerchantId` int(11) NOT NULL,
  `ScrapedAt` datetime(6) NOT NULL,
  `Status` longtext NOT NULL,
  `ErrorMessage` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `merchants`
--

CREATE TABLE `merchants` (
  `MerchantId` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Url` longtext NOT NULL,
  `ContactEmail` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `merchants`
--

INSERT INTO `merchants` (`MerchantId`, `Name`, `Url`, `ContactEmail`) VALUES
(1, 'Chadwicks', 'https://www.chadwicks.ie', 'support@chadwicks.ie'),
(2, 'TJOMahony', 'https://www.tjomahony.ie', 'contact@tjomahony.ie'),
(3, 'CorkBP', 'https://www.corkbp.ie', 'N/A');

-- --------------------------------------------------------

--
-- Table structure for table `prices`
--

CREATE TABLE `prices` (
  `PriceId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `MerchantId` int(11) NOT NULL,
  `PriceValue` decimal(65,30) NOT NULL,
  `ScrapedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `prices`
--


-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `ProductId` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Description` longtext NOT NULL,
  `Category` longtext NOT NULL,
  `Unit` longtext NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `UpdatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `products`
--



-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20250105131816_InitialCreate', '6.0.33');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `loggings`
--
ALTER TABLE `loggings`
  ADD PRIMARY KEY (`ScrapeId`),
  ADD KEY `IX_Loggings_MerchantId` (`MerchantId`);

--
-- Indexes for table `merchants`
--
ALTER TABLE `merchants`
  ADD PRIMARY KEY (`MerchantId`);

--
-- Indexes for table `prices`
--
ALTER TABLE `prices`
  ADD PRIMARY KEY (`PriceId`),
  ADD KEY `IX_Prices_MerchantId` (`MerchantId`),
  ADD KEY `IX_Prices_ProductId` (`ProductId`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductId`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `loggings`
--
ALTER TABLE `loggings`
  MODIFY `ScrapeId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `merchants`
--
ALTER TABLE `merchants`
  MODIFY `MerchantId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `prices`
--
ALTER TABLE `prices`
  MODIFY `PriceId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ProductId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `loggings`
--
ALTER TABLE `loggings`
  ADD CONSTRAINT `FK_Loggings_Merchants_MerchantId` FOREIGN KEY (`MerchantId`) REFERENCES `merchants` (`MerchantId`) ON DELETE CASCADE;

--
-- Constraints for table `prices`
--
ALTER TABLE `prices`
  ADD CONSTRAINT `FK_Prices_Merchants_MerchantId` FOREIGN KEY (`MerchantId`) REFERENCES `merchants` (`MerchantId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Prices_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`ProductId`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
