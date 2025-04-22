-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 22, 2025 at 01:34 PM
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

--
-- Dumping data for table `loggings`
--

INSERT INTO `loggings` (`ScrapeId`, `MerchantId`, `ScrapedAt`, `Status`, `ErrorMessage`) VALUES
(1, 1, '2025-04-22 09:47:50.113854', 'Success', 'Scraped successfully for Chadwicks'),
(2, 2, '2025-04-22 09:50:15.182198', 'failed', 'TJOMahony scraper failedExecution context was destroyed, most likely because of a navigation.'),
(3, 3, '2025-04-22 09:51:17.680816', 'Success', 'Scraped successfully for CorkBP'),
(4, 2, '2025-04-22 09:54:57.496267', 'Success', 'Scraped successfully for TJOMahony'),
(5, 1, '2025-04-22 10:05:54.646316', 'Success', 'Partially scraped successfully for Chadwicks'),
(6, 2, '2025-04-22 10:09:10.870500', 'Success', 'Partially scraped successfully for TJOMahony'),
(7, 3, '2025-04-22 10:10:00.545024', 'Success', 'Partially scraped successfully for CorkBP'),
(8, 1, '2025-04-22 10:13:08.005323', 'failed', 'Chadwicks scraper failed: Node is either not clickable or not an Element'),
(9, 1, '2025-04-22 10:14:59.834563', 'Success', 'Partially scraped successfully for Chadwicks');

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
  `ScrapedAt` datetime(6) NOT NULL,
  `ProductUrl` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `prices`
--

INSERT INTO `prices` (`PriceId`, `ProductId`, `MerchantId`, `PriceValue`, `ScrapedAt`, `ProductUrl`) VALUES
(6, 6, 1, 195.720000000000000000000000000000, '2025-04-22 10:13:55.785043', 'https://www.chadwicks.ie/superfoil-insulation-1-5-x-10m-15m2-21450.html'),
(7, 7, 1, 14.700000000000000000000000000000, '2025-04-22 10:13:55.785284', 'https://www.chadwicks.ie/gyproc-metal-stud-3600mm-70-s-50-27126.html'),
(8, 8, 1, 10.250000000000000000000000000000, '2025-04-22 09:47:32.957257', 'https://www.chadwicks.ie/kpro-crete-3-1-sand-cement-34397.html'),
(9, 9, 1, 6.220000000000000000000000000000, '2025-04-22 10:13:55.785416', 'https://www.chadwicks.ie/cashel-40-x-400-x-400mm-natural-paving-slab-26432.html'),
(10, 10, 1, 14.170000000000000000000000000000, '2025-04-22 10:13:55.785466', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-44-rough-white-deal-treated-14014.html'),
(11, 11, 1, 5.840000000000000000000000000000, '2025-04-22 10:13:55.785508', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/2-4m-100-x-44-rough-white-deal-14559.html'),
(12, 12, 1, 21.130000000000000000000000000000, '2025-04-22 10:13:55.785551', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-150-x-44-rough-white-deal-treated-14020.html'),
(13, 13, 1, 7.020000000000000000000000000000, '2025-04-22 10:13:55.785594', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-22-rough-white-deal-treated-14003.html'),
(14, 14, 1, 3.190000000000000000000000000000, '2025-04-22 10:14:02.276305', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-50-x-22-rough-white-deal-treated-14053.html'),
(15, 15, 1, 10.570000000000000000000000000000, '2025-04-22 10:13:55.785675', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-150-x-22-rough-white-deal-treated-16327.html'),
(16, 16, 1, 5.550000000000000000000000000000, '2025-04-22 10:13:55.785710', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-50-x-35-rough-white-deal-treated-14063.html'),
(17, 17, 1, 9.440000000000000000000000000000, '2025-04-22 10:13:55.785744', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-75-x-44-rough-white-deal-14552.html'),
(18, 18, 1, 6.960000000000000000000000000000, '2025-04-22 10:13:55.785783', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-50-x-44-rough-white-deal-treated-14007.html'),
(19, 19, 1, 11.690000000000000000000000000000, '2025-04-22 10:13:55.785818', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-44-rough-white-deal-14566.html'),
(20, 20, 1, 8.770000000000000000000000000000, '2025-04-22 10:13:55.785861', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-100-x-44-rough-white-deal-14562.html'),
(21, 21, 1, 24.140000000000000000000000000000, '2025-04-22 10:13:55.785919', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-75-rough-white-deal-treated-14081.html'),
(22, 22, 1, 12.340000000000000000000000000000, '2025-04-22 10:13:58.456915', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-175-x-22-rough-white-deal-treated-14004.html'),
(23, 23, 1, 6.320000000000000000000000000000, '2025-04-22 10:13:58.457096', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-50-x-44-rough-white-deal-14538.html'),
(24, 24, 1, 13.230000000000000000000000000000, '2025-04-22 10:13:58.457130', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-150-x-44-rough-white-deal-14620.html'),
(25, 25, 1, 6.320000000000000000000000000000, '2025-04-22 10:13:58.457154', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-22-rough-white-deal-14238.html'),
(26, 26, 1, 35.950000000000000000000000000000, '2025-04-22 10:13:58.457176', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-150-x-75-rough-white-deal-treated-14015.html'),
(27, 27, 1, 15.820000000000000000000000000000, '2025-04-22 10:13:58.457198', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-225-x-22-rough-white-deal-treated-14009.html'),
(28, 28, 1, 13.150000000000000000000000000000, '2025-04-22 10:13:58.457220', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-4m-100-x-44-rough-white-deal-14568.html'),
(29, 29, 1, 14.590000000000000000000000000000, '2025-04-22 10:13:58.457241', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-125-x-44-rough-white-deal-14608.html'),
(30, 30, 1, 13.280000000000000000000000000000, '2025-04-22 10:13:58.457263', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-225-x-22-rough-white-deal-14324.html'),
(31, 31, 1, 5.080000000000000000000000000000, '2025-04-22 10:13:58.457288', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-50-x-35-rough-white-deal-14380.html'),
(32, 32, 1, 19.360000000000000000000000000000, '2025-04-22 10:13:58.457308', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-75-rough-white-deal-14738.html'),
(33, 33, 1, 10.570000000000000000000000000000, '2025-04-22 10:13:58.457328', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-75-x-44-rough-white-deal-treated-14073.html'),
(34, 34, 1, 3.190000000000000000000000000000, '2025-04-22 09:47:35.071054', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-50-x-22-rough-white-deal-14224.html'),
(35, 35, 1, 11.220000000000000000000000000000, '2025-04-22 10:14:02.276435', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-175-x-22-rough-white-deal-14294.html'),
(36, 36, 1, 15.450000000000000000000000000000, '2025-04-22 10:14:02.276474', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-150-x-44-rough-white-deal-14622.html'),
(37, 37, 1, 4.720000000000000000000000000000, '2025-04-22 10:14:02.276506', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/2-4m-75-x-44-rough-white-deal-14545.html'),
(38, 38, 1, 10.930000000000000000000000000000, '2025-04-22 10:14:02.276527', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-125-x-44-rough-white-deal-14604.html'),
(39, 39, 1, 17.640000000000000000000000000000, '2025-04-22 10:14:02.276551', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-150-x-44-rough-white-deal-14624.html'),
(40, 40, 1, 31.710000000000000000000000000000, '2025-04-22 10:14:02.276571', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-225-x-44-rough-white-deal-treated-16330.html'),
(41, 41, 1, 29.030000000000000000000000000000, '2025-04-22 10:14:02.276592', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-4m-225-x-44-rough-white-deal-14674.html'),
(42, 42, 1, 19.860000000000000000000000000000, '2025-04-22 10:14:02.276611', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-4m-150-x-44-rough-white-deal-14626.html'),
(43, 43, 1, 16.410000000000000000000000000000, '2025-04-22 10:14:02.276631', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-4m-125-x-44-rough-white-deal-14610.html'),
(44, 44, 1, 3.380000000000000000000000000000, '2025-04-22 10:14:02.276652', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-5m-50-x-22-rough-white-deal-treated-14052.html'),
(45, 45, 1, 7.310000000000000000000000000000, '2025-04-22 10:14:02.276672', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-0m-100-x-44-rough-white-deal-14560.html'),
(46, 46, 1, 5.900000000000000000000000000000, '2025-04-22 10:14:06.480208', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-0m-75-x-44-rough-white-deal-14546.html'),
(47, 47, 1, 25.810000000000000000000000000000, '2025-04-22 10:14:06.480295', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-225-x-44-rough-white-deal-14672.html'),
(48, 48, 1, 38.810000000000000000000000000000, '2025-04-22 10:14:06.480318', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/6-0m-150-x-44-rough-white-deal-14628.html'),
(49, 49, 1, 43.980000000000000000000000000000, '2025-04-22 10:14:06.480338', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-225-x-75-rough-white-deal-14824.html'),
(50, 50, 1, 11.160000000000000000000000000000, '2025-04-22 10:14:06.480357', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-35-white-deal-rough-treated-14146.html'),
(51, 51, 1, 10.630000000000000000000000000000, '2025-04-22 10:14:06.480387', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-4m-75-x-44-rough-white-deal-14554.html'),
(52, 52, 1, 19.350000000000000000000000000000, '2025-04-22 10:14:06.480487', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-225-x-44-rough-white-deal-14668.html'),
(53, 53, 1, 15.060000000000000000000000000000, '2025-04-22 10:14:06.480512', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-1m-100-x-44-rough-white-deal-treated-14026.html'),
(54, 54, 1, 22.570000000000000000000000000000, '2025-04-22 10:14:06.480535', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-225-x-44-rough-white-deal-14670.html'),
(55, 55, 1, 10.230000000000000000000000000000, '2025-04-22 10:14:06.480555', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-100-x-44-rough-white-deal-14564.html'),
(56, 56, 1, 24.740000000000000000000000000000, '2025-04-22 10:14:06.480580', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-175-x-44-rough-white-deal-treated-14046.html'),
(57, 57, 1, 22.520000000000000000000000000000, '2025-04-22 10:14:06.480601', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-4m-175-x-44-rough-white-deal-14640.html'),
(58, 58, 1, 3.840000000000000000000000000000, '2025-04-22 10:14:10.716489', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/2-4m-75-x-35-rough-white-deal-14386.html'),
(59, 59, 1, 15.010000000000000000000000000000, '2025-04-22 10:14:10.716582', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-175-x-44-rough-white-deal-14634.html'),
(60, 60, 1, 17.520000000000000000000000000000, '2025-04-22 10:14:10.716606', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-175-x-44-rough-white-deal-14636.html'),
(61, 61, 1, 7.080000000000000000000000000000, '2025-04-22 10:14:10.716635', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-75-x-44-rough-white-deal-14548.html'),
(62, 62, 1, 11.030000000000000000000000000000, '2025-04-22 10:14:10.716655', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-0m-150-x-44-rough-white-deal-14618.html'),
(63, 63, 1, 24.440000000000000000000000000000, '2025-04-22 10:14:10.716678', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-75-x-44-furring-piece-cut-diag-2-x-4-8m-pcs-14934.html'),
(64, 64, 1, 21.250000000000000000000000000000, '2025-04-22 10:14:10.716709', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-44-furring-piece-cut-diag-2-x-4-8m-pcs-14918.html'),
(65, 65, 1, 7.080000000000000000000000000000, '2025-04-22 10:14:10.716729', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/2-4m-100-x-44-rough-white-deal-treated-14214.html'),
(66, 66, 1, 6.590000000000000000000000000000, '2025-04-22 10:14:10.716753', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-5m-100-x-22-rough-white-deal-treated-14016.html'),
(67, 67, 1, 54.080000000000000000000000000000, '2025-04-22 10:14:10.716772', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-225-x-75-white-deal-rough-treated-16322.html'),
(68, 68, 1, 49.480000000000000000000000000000, '2025-04-22 10:14:10.716791', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-4m-225-x-75-rough-white-deal-14826.html'),
(69, 69, 1, 34.180000000000000000000000000000, '2025-04-22 10:14:10.716809', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-175-x-75-rough-white-deal-14794.html'),
(70, 70, 1, 29.220000000000000000000000000000, '2025-04-22 10:14:15.854803', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-150-x-75-rough-white-deal-14780.html'),
(71, 71, 1, 16.130000000000000000000000000000, '2025-04-22 10:14:15.854928', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-0m-225-x-44-rough-white-deal-14666.html'),
(72, 72, 1, 42.400000000000000000000000000000, '2025-04-22 10:14:15.854953', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/6-0m-175-x-44-rough-white-deal-14642.html'),
(73, 73, 1, 20.010000000000000000000000000000, '2025-04-22 10:14:15.854974', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-175-x-44-rough-white-deal-14638.html'),
(74, 74, 1, 12.510000000000000000000000000000, '2025-04-22 10:14:15.854994', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-0m-175-x-44-rough-white-deal-14632.html'),
(75, 75, 1, 12.760000000000000000000000000000, '2025-04-22 10:14:15.855013', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-125-x-44-rough-white-deal-14606.html'),
(76, 76, 1, 8.270000000000000000000000000000, '2025-04-22 10:14:15.855034', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-75-x-44-rough-white-deal-14550.html'),
(77, 77, 1, 5.420000000000000000000000000000, '2025-04-22 10:14:15.855054', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/2-7m-75-x-44-rough-white-deal-14544.html'),
(78, 78, 1, 10.800000000000000000000000000000, '2025-04-22 10:14:15.855073', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-115-x-35-rough-white-deal-14424.html'),
(79, 79, 1, 9.980000000000000000000000000000, '2025-04-22 10:14:15.855092', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-35-rough-white-deal-14410.html'),
(80, 80, 1, 7.680000000000000000000000000000, '2025-04-22 10:14:15.855112', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-75-x-35-rough-white-deal-14394.html'),
(81, 81, 1, 2.990000000000000000000000000000, '2025-04-22 10:14:15.855131', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-5m-50-x-22-rough-white-deal-14223.html'),
(82, 82, 1, 11.120000000000000000000000000000, '2025-04-22 10:14:20.464886', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/2-7m-75-x-44-imported-rough-white-deal-15375.html'),
(83, 83, 1, 2.420000000000000000000000000000, '2025-04-22 10:14:20.465008', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/100-x-35-imported-white-deal-rough-wdr-15373.html'),
(84, 84, 1, 7.520000000000000000000000000000, '2025-04-22 10:14:20.465030', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/150-x-44-imported-rough-white-deal-treated-14897.html'),
(85, 85, 1, 40.580000000000000000000000000000, '2025-04-22 10:14:20.465049', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-4m-150-x-44-imported-rough-white-deal-treated-14896.html'),
(86, 86, 1, 9.520000000000000000000000000000, '2025-04-22 10:14:20.465067', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-50-x-35-imported-rough-white-deal-14895.html'),
(87, 87, 1, 40.900000000000000000000000000000, '2025-04-22 10:14:20.465085', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-75-imported-rough-white-deal-treated-14881.html'),
(88, 88, 1, 8.520000000000000000000000000000, '2025-04-22 10:14:20.465101', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/100-x-75-imported-rough-white-deal-treated-14880.html'),
(89, 89, 1, 11.230000000000000000000000000000, '2025-04-22 10:14:20.465119', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-1m-50-x-36-imported-white-deal-rough-sr82-14866.html'),
(90, 90, 1, 20.430000000000000000000000000000, '2025-04-22 10:14:20.465136', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-35-imported-rough-white-deal-treated-14847.html'),
(91, 91, 1, 4.260000000000000000000000000000, '2025-04-22 10:14:20.465162', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/100-x-35-imported-rough-white-deal-treated-14846.html'),
(92, 92, 1, 11.460000000000000000000000000000, '2025-04-22 10:14:20.465197', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-50-x-44-imported-rough-white-deal-treated-14770.html'),
(93, 93, 1, 2.730000000000000000000000000000, '2025-04-22 10:14:20.465217', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/50-x-44-imported-rough-white-deal-treated-14769.html'),
(94, 94, 1, 24.220000000000000000000000000000, '2025-04-22 10:14:28.634336', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-1m-100-x-44-imported-rough-white-deal-14766.html'),
(95, 95, 1, 40.960000000000000000000000000000, '2025-04-22 10:14:28.634427', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-1m-175-x-44-imported-rough-white-deal-14765.html'),
(96, 96, 1, 23.790000000000000000000000000000, '2025-04-22 10:14:28.634450', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-3m-150-x-44-imported-rough-white-deal-14696.html'),
(97, 97, 1, 54.390000000000000000000000000000, '2025-04-22 10:14:28.634467', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-1m-225-x-44-truss-timber-c24-rough-white-deal-17214.html'),
(98, 98, 1, 38.390000000000000000000000000000, '2025-04-22 10:14:28.634486', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-225-x-44-truss-timber-c24-rough-white-deal-17212.html'),
(99, 99, 1, 26.300000000000000000000000000000, '2025-04-22 10:14:28.634503', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-150-x-35-imported-rough-white-deal-14841.html'),
(100, 100, 1, 125.630000000000000000000000000000, '2025-04-22 10:14:28.634531', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-7m-225-x-75-imported-rough-white-deal-14817.html'),
(101, 101, 1, 82.620000000000000000000000000000, '2025-04-22 10:14:28.634551', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-1m-225-x-75-imported-rough-white-deal-14814.html'),
(102, 102, 1, 72.900000000000000000000000000000, '2025-04-22 10:14:28.634567', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-5m-225-x-75-imported-rough-white-deal-14813.html'),
(103, 103, 1, 68.030000000000000000000000000000, '2025-04-22 10:14:28.634584', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-225-x-75-imported-rough-white-deal-14812.html'),
(104, 104, 1, 48.600000000000000000000000000000, '2025-04-22 10:14:28.634599', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-0m-225-x-75-imported-rough-white-deal-14811.html'),
(105, 105, 1, 31.330000000000000000000000000000, '2025-04-22 10:14:28.634616', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-9m-175-x-44-imported-rough-white-deal-14810.html'),
(106, 106, 1, 26.400000000000000000000000000000, '2025-04-22 10:14:35.837002', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-125-x-44-imported-rough-white-deal-14809.html'),
(107, 107, 1, 6.670000000000000000000000000000, '2025-04-22 10:14:35.837093', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-50-x-36-imported-wh-deal-rough-sr82-treated-wc-14807.html'),
(108, 108, 1, 26.510000000000000000000000000000, '2025-04-22 10:14:35.837118', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-3m-175-x-44-imported-rough-white-deal-14659.html'),
(109, 109, 1, 23.200000000000000000000000000000, '2025-04-22 10:14:35.837136', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-100-x-44-imported-rough-white-deal-14598.html'),
(110, 110, 1, 13.260000000000000000000000000000, '2025-04-22 10:14:35.837152', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/2-4m-100-x-44-imported-rough-white-deal-14591.html'),
(111, 111, 1, 36.150000000000000000000000000000, '2025-04-22 10:14:35.837189', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-5m-175-x-44-imported-rough-white-deal-14499.html'),
(112, 112, 1, 25.950000000000000000000000000000, '2025-04-22 10:14:35.837210', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-150-x-44-imported-rough-white-deal-14498.html'),
(113, 113, 1, 17.300000000000000000000000000000, '2025-04-22 10:14:35.837232', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-75-x-44-imported-rough-white-deal-14492.html'),
(114, 114, 1, 32.440000000000000000000000000000, '2025-04-22 10:14:35.837251', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-5m-150-x-44-imported-rough-white-deal-14491.html'),
(115, 115, 1, 21.620000000000000000000000000000, '2025-04-22 10:14:35.837283', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-0m-150-x-44-imported-rough-white-deal-14490.html'),
(116, 116, 1, 24.510000000000000000000000000000, '2025-04-22 10:14:35.837304', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-9m-125-x-44-imported-rough-white-deal-14489.html'),
(117, 117, 1, 11.370000000000000000000000000000, '2025-04-22 10:14:35.837326', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-50-x-44-imported-rough-white-deal-14416.html'),
(118, 118, 1, 72.020000000000000000000000000000, '2025-04-22 10:14:41.871512', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-5m-225-x-44-imported-rough-white-deal-14403.html'),
(119, 119, 1, 36.760000000000000000000000000000, '2025-04-22 10:14:41.871595', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-1m-150-x-44-imported-rough-white-deal-14398.html'),
(120, 120, 1, 22.630000000000000000000000000000, '2025-04-22 10:14:41.871616', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-125-x-44-imported-rough-white-deal-14397.html'),
(121, 121, 1, 1.590000000000000000000000000000, '2025-04-22 10:14:41.871634', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/50-x-36-imported-white-deal-rough-sr82-treated-14373.html'),
(122, 122, 1, 2.200000000000000000000000000000, '2025-04-22 10:14:41.871651', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/50-x-36-imported-white-deal-rough-sr82-14372.html'),
(123, 123, 1, 103.970000000000000000000000000000, '2025-04-22 10:14:41.871668', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/7-8m-225-x-44-truss-timber-c24-rough-white-deal-14356.html'),
(124, 124, 1, 20.430000000000000000000000000000, '2025-04-22 10:14:41.871685', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-150-x-22-imported-rough-white-deal-14355.html'),
(125, 125, 1, 98.330000000000000000000000000000, '2025-04-22 10:14:41.871702', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/7-2m-225-x-44-truss-timber-c24-rough-white-deal-14352.html'),
(126, 126, 1, 23.090000000000000000000000000000, '2025-04-22 10:14:41.871719', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-175-x-22-imported-rough-white-deal-14344.html'),
(127, 127, 1, 13.810000000000000000000000000000, '2025-04-22 10:14:41.871736', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-22-imported-rough-white-deal-treated-14341.html'),
(128, 128, 1, 28.920000000000000000000000000000, '2025-04-22 10:14:41.871752', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-6m-175-x-44-imported-rough-white-deal-14338.html'),
(129, 129, 1, 7.610000000000000000000000000000, '2025-04-22 10:14:41.871767', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-50-x-36-imported-wh-deal-rough-sr82-treated-14303.html'),
(130, 130, 1, 10.570000000000000000000000000000, '2025-04-22 10:14:47.528041', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-50-x-36-imported-white-deal-rough-sr82-14302.html'),
(131, 131, 1, 9.910000000000000000000000000000, '2025-04-22 10:14:47.528131', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-5m-50-x-36-imported-white-deal-rough-sr82-14301.html'),
(132, 132, 1, 9.250000000000000000000000000000, '2025-04-22 10:14:47.528155', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-50-x-36-imported-white-deal-rough-sr82-14300.html'),
(133, 133, 1, 28.400000000000000000000000000000, '2025-04-22 10:14:47.528174', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-225-x-22-imported-rough-white-deal-14299.html'),
(134, 134, 1, 43.460000000000000000000000000000, '2025-04-22 10:14:47.528193', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-225-x-35-imported-rough-white-deal-14298.html'),
(135, 135, 1, 9.050000000000000000000000000000, '2025-04-22 10:14:47.528212', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/225-x-35-imported-rough-white-deal-14270.html'),
(136, 136, 1, 77.640000000000000000000000000000, '2025-04-22 10:14:47.528229', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-7m-225-x-44-imported-rough-white-deal-14267.html'),
(137, 137, 1, 81.610000000000000000000000000000, '2025-04-22 10:14:47.528247', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-1m-225-x-44-imported-rough-white-deal-14265.html'),
(138, 138, 1, 62.410000000000000000000000000000, '2025-04-22 10:14:47.528264', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-9m-225-x-44-imported-rough-white-deal-14263.html'),
(139, 139, 1, 60.060000000000000000000000000000, '2025-04-22 10:14:47.528282', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/6-0m-175-x-44-truss-timber-c24-rough-white-deal-14260.html'),
(140, 140, 1, 52.800000000000000000000000000000, '2025-04-22 10:14:47.528301', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-3m-225-x-44-imported-rough-white-deal-14258.html'),
(141, 141, 1, 48.010000000000000000000000000000, '2025-04-22 10:14:47.528319', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-0m-225-x-44-imported-rough-white-deal-14257.html'),
(142, 142, 1, 30.170000000000000000000000000000, '2025-04-22 10:14:52.763511', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-125-x-44-imported-rough-white-deal-14255.html'),
(143, 143, 1, 28.290000000000000000000000000000, '2025-04-22 10:14:52.763601', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-5m-125-x-44-imported-rough-white-deal-14254.html'),
(144, 144, 1, 38.920000000000000000000000000000, '2025-04-22 10:14:52.763624', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/5-4m-150-x-44-imported-rough-white-deal-14253.html'),
(145, 145, 1, 30.270000000000000000000000000000, '2025-04-22 10:14:52.763643', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-150-x-44-imported-rough-white-deal-14251.html'),
(146, 146, 1, 28.110000000000000000000000000000, '2025-04-22 10:14:52.763662', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/3-9m-150-x-44-imported-rough-white-deal-14250.html'),
(147, 147, 1, 79.980000000000000000000000000000, '2025-04-22 10:14:52.763680', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/6-0m-225-x-44-truss-timber-c24-rough-white-deal-14249.html'),
(148, 148, 1, 33.740000000000000000000000000000, '2025-04-22 10:14:52.763699', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-2m-175-x-44-imported-rough-white-deal-14212.html'),
(149, 149, 1, 5.920000000000000000000000000000, '2025-04-22 10:14:52.763721', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/225-x-22-imported-rough-white-deal-14197.html'),
(150, 150, 1, 4.800000000000000000000000000000, '2025-04-22 10:14:52.763738', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/175-x-22-imported-rough-white-deal-14196.html'),
(151, 151, 1, 4.260000000000000000000000000000, '2025-04-22 10:14:52.763756', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/150-x-22-imported-rough-white-deal-14190.html'),
(152, 152, 1, 4.120000000000000000000000000000, '2025-04-22 10:14:52.763772', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/75-x-44-imported-rough-white-deal-14186.html'),
(153, 153, 1, 42.290000000000000000000000000000, '2025-04-22 10:14:52.763789', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-100-x-75-imported-rough-white-deal-13980.html'),
(154, 154, 1, 192.000000000000000000000000000000, '2025-04-22 10:14:57.101757', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/steico-i-joist-45-x-300mm-x-13m-15592.html'),
(155, 155, 1, 173.950000000000000000000000000000, '2025-04-22 10:14:57.101852', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/steico-i-joist-45-x-240mm-x-13m-15591.html'),
(156, 156, 1, 59.580000000000000000000000000000, '2025-04-22 10:14:57.101873', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/6-6m-225-x-44-rough-white-deal-14678.html'),
(157, 157, 1, 53.070000000000000000000000000000, '2025-04-22 10:14:57.101892', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/6-0m-225-x-44-rough-white-deal-14676.html'),
(158, 158, 1, 20.490000000000000000000000000000, '2025-04-22 10:14:57.101908', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/4-8m-225-x-35-rough-white-deal-14510.html'),
(159, 159, 1, 25.260000000000000000000000000000, '2025-04-22 10:14:57.101927', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/rough-white-deal-treated-timber-225mm-x-35mm-x-4-8m-9-x-1-5-inches-16352.html'),
(160, 160, 1, 19.660000000000000000000000000000, '2025-04-22 10:14:57.101945', 'https://www.chadwicks.ie/all-products/timber-1/rough-timber/rough-white-deal-treated-timber-175mm-x-35mm-x-4-8m-7-x-1-5-inches-16328.html'),
(161, 10, 3, 13.990000000000000000000000000000, '2025-04-22 10:09:38.225258', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-100-x-44-rough-white-deal-treated-14014.html'),
(162, 11, 3, 5.570000000000000000000000000000, '2025-04-22 10:09:38.225351', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/2-4m-100-x-44-rough-white-deal-14559.html'),
(163, 12, 3, 21.020000000000000000000000000000, '2025-04-22 10:09:38.225376', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-150-x-44-rough-white-deal-treated-14020.html'),
(164, 13, 3, 6.920000000000000000000000000000, '2025-04-22 10:09:38.225402', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-100-x-22-rough-white-deal-treated-14003.html'),
(165, 15, 3, 10.500000000000000000000000000000, '2025-04-22 10:09:38.225426', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-150-x-22-rough-white-deal-treated-16327.html'),
(166, 16, 3, 5.560000000000000000000000000000, '2025-04-22 10:09:38.225449', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-50-x-35-rough-white-deal-treated-14063.html'),
(167, 17, 3, 8.360000000000000000000000000000, '2025-04-22 10:09:38.225472', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-75-x-44-rough-white-deal-14552.html'),
(168, 19, 3, 11.170000000000000000000000000000, '2025-04-22 10:09:38.225494', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-100-x-44-rough-white-deal-14566.html'),
(169, 20, 3, 8.360000000000000000000000000000, '2025-04-22 10:09:38.225515', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/3-6m-100-x-44-rough-white-deal-14562.html'),
(170, 21, 3, 23.870000000000000000000000000000, '2025-04-22 10:09:38.225537', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-100-x-75-rough-white-deal-treated-14081.html'),
(171, 24, 3, 12.550000000000000000000000000000, '2025-04-22 10:09:38.225560', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/3-6m-150-x-44-rough-white-deal-14620.html'),
(172, 25, 3, 5.570000000000000000000000000000, '2025-04-22 10:09:38.225584', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-100-x-22-rough-white-deal-14238.html'),
(173, 26, 3, 35.820000000000000000000000000000, '2025-04-22 10:09:41.739813', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-150-x-75-rough-white-deal-treated-14015.html'),
(174, 27, 3, 15.770000000000000000000000000000, '2025-04-22 10:09:41.739911', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-225-x-22-rough-white-deal-treated-14009.html'),
(175, 28, 3, 12.550000000000000000000000000000, '2025-04-22 10:09:41.739934', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/5-4m-100-x-44-rough-white-deal-14568.html'),
(176, 29, 3, 13.950000000000000000000000000000, '2025-04-22 10:09:41.739952', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-125-x-44-rough-white-deal-14608.html'),
(177, 30, 3, 12.550000000000000000000000000000, '2025-04-22 10:09:41.739970', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-225-x-22-rough-white-deal-14324.html'),
(178, 32, 3, 19.020000000000000000000000000000, '2025-04-22 10:09:41.739988', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-100-x-75-rough-white-deal-14738.html'),
(179, 33, 3, 10.500000000000000000000000000000, '2025-04-22 10:09:41.740007', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-75-x-44-rough-white-deal-treated-14073.html'),
(180, 35, 3, 9.770000000000000000000000000000, '2025-04-22 10:09:41.740024', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-175-x-22-rough-white-deal-14294.html'),
(181, 36, 3, 14.640000000000000000000000000000, '2025-04-22 10:09:41.740042', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-2m-150-x-44-rough-white-deal-14622.html'),
(182, 37, 3, 4.180000000000000000000000000000, '2025-04-22 10:09:41.740060', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/2-4m-75-x-44-rough-white-deal-14545.html'),
(183, 38, 3, 10.460000000000000000000000000000, '2025-04-22 10:09:41.740078', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/3-6m-125-x-44-rough-white-deal-14604.html'),
(184, 39, 3, 16.730000000000000000000000000000, '2025-04-22 10:09:41.740096', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-150-x-44-rough-white-deal-14624.html'),
(185, 40, 3, 31.510000000000000000000000000000, '2025-04-22 10:09:45.605212', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-225-x-44-rough-white-deal-treated-16330.html'),
(186, 41, 3, 28.250000000000000000000000000000, '2025-04-22 10:09:45.605316', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/5-4m-225-x-44-rough-white-deal-14674.html'),
(187, 42, 3, 18.820000000000000000000000000000, '2025-04-22 10:09:45.605339', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/5-4m-150-x-44-rough-white-deal-14626.html'),
(188, 44, 3, 3.260000000000000000000000000000, '2025-04-22 10:09:45.605358', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-5m-50-x-22-rough-white-deal-treated-14052.html'),
(189, 45, 3, 6.970000000000000000000000000000, '2025-04-22 10:09:45.605377', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/3-0m-100-x-44-rough-white-deal-14560.html'),
(190, 46, 3, 5.230000000000000000000000000000, '2025-04-22 10:09:45.605396', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/3-0m-75-x-44-rough-white-deal-14546.html'),
(191, 47, 3, 25.100000000000000000000000000000, '2025-04-22 10:09:45.605419', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-225-x-44-rough-white-deal-14672.html'),
(192, 49, 3, 42.780000000000000000000000000000, '2025-04-22 10:09:45.605438', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-225-x-75-rough-white-deal-14824.html'),
(193, 52, 3, 18.820000000000000000000000000000, '2025-04-22 10:09:45.605475', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/3-6m-225-x-44-rough-white-deal-14668.html'),
(194, 54, 3, 21.970000000000000000000000000000, '2025-04-22 10:09:45.605492', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-2m-225-x-44-rough-white-deal-14670.html'),
(195, 57, 3, 21.970000000000000000000000000000, '2025-04-22 10:09:45.605509', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/5-4m-175-x-44-rough-white-deal-14640.html'),
(196, 59, 3, 14.640000000000000000000000000000, '2025-04-22 10:09:50.473080', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/3-6m-175-x-44-rough-white-deal-14634.html'),
(197, 60, 3, 17.070000000000000000000000000000, '2025-04-22 10:09:50.473159', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-2m-175-x-44-rough-white-deal-14636.html'),
(198, 67, 3, 53.710000000000000000000000000000, '2025-04-22 10:09:50.473182', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-225-x-75-white-deal-rough-treated-16322.html'),
(199, 68, 3, 48.130000000000000000000000000000, '2025-04-22 10:09:50.473201', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/5-4m-225-x-75-rough-white-deal-14826.html'),
(200, 70, 3, 28.520000000000000000000000000000, '2025-04-22 10:09:50.473219', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-150-x-75-rough-white-deal-14780.html'),
(201, 71, 3, 15.680000000000000000000000000000, '2025-04-22 10:09:50.473239', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/3-0m-225-x-44-rough-white-deal-14666.html'),
(202, 73, 3, 19.520000000000000000000000000000, '2025-04-22 10:09:50.473257', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-175-x-44-rough-white-deal-14638.html'),
(203, 74, 3, 12.200000000000000000000000000000, '2025-04-22 10:09:50.473273', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/3-0m-175-x-44-rough-white-deal-14632.html'),
(204, 76, 3, 7.320000000000000000000000000000, '2025-04-22 10:09:50.473290', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-2m-75-x-44-rough-white-deal-14550.html'),
(205, 81, 3, 2.620000000000000000000000000000, '2025-04-22 10:09:50.473324', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-5m-50-x-22-rough-white-deal-14223.html'),
(206, 158, 3, 19.960000000000000000000000000000, '2025-04-22 10:09:55.453129', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-225-x-35-rough-white-deal-14510.html'),
(207, 161, 3, 5.230000000000000000000000000000, '2025-04-22 10:09:45.605457', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-5m-50-x-44-rough-white-deal-14537.html'),
(208, 162, 3, 4.160000000000000000000000000000, '2025-04-22 10:09:50.473306', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-5m-50-x-35-rough-white-deal-14379.html'),
(209, 163, 3, 566.490000000000000000000000000000, '2025-04-22 09:51:03.341938', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/carbery-1350l-non-potable-water-tank-black-96214.html'),
(210, 164, 3, 6.680000000000000000000000000000, '2025-04-22 10:09:55.452956', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-50-x-35-sr82-rough-white-deal-batten-treated-14805.html'),
(211, 165, 3, 18.820000000000000000000000000000, '2025-04-22 10:09:55.453038', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-150-x-50-cls-white-deal-14704.html'),
(212, 166, 3, 12.550000000000000000000000000000, '2025-04-22 10:09:55.453061', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-100-x-50-cls-white-deal-14701.html'),
(213, 167, 3, 6.150000000000000000000000000000, '2025-04-22 10:09:55.453079', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-2m-50-x-44-rough-white-deal-treated-14006.html'),
(214, 168, 3, 22.120000000000000000000000000000, '2025-04-22 10:09:55.453096', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/scaffolding-plank-1-25m-225-x-63mm-20607.html'),
(215, 169, 3, 20.590000000000000000000000000000, '2025-04-22 10:09:55.453113', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-150-x-50-cls-white-deal-treated-14710.html'),
(216, 170, 3, 15.530000000000000000000000000000, '2025-04-22 10:09:55.453152', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-175-x-35-rough-white-deal-14474.html'),
(217, 171, 3, 39.950000000000000000000000000000, '2025-04-22 10:09:55.453181', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/6-6m-225-x-44-imported-rough-white-deal-14157.html'),
(218, 172, 3, 35.340000000000000000000000000000, '2025-04-22 10:09:55.453211', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/6-0m-225-x-44-imported-rough-white-deal-14156.html'),
(219, 173, 3, 8.350000000000000000000000000000, '2025-04-22 10:09:55.453231', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-8m-75-x-35-white-deal-rough-treated-14113.html'),
(220, 174, 3, 5.230000000000000000000000000000, '2025-04-22 10:09:55.453248', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/4-5m-50-x-35-rough-white-deal-treated-14066.html'),
(221, 175, 3, 3.690000000000000000000000000000, '2025-04-22 10:09:59.359735', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/5-1m-50-x-22-rough-white-deal-treated-14054.html'),
(222, 176, 3, 27.580000000000000000000000000000, '2025-04-22 10:09:59.359815', 'https://www.corkbp.ie/all-products/timber-1/rough-timber/5-4m-175-x-44-white-deal-rough-treated-14143.html'),
(223, 58, 2, 3.500000000000000000000000000000, '2025-04-22 10:06:19.254516', 'https://tjomahony.ie/75mm-x-35mm-rough-timber-3-x-1-5-2-4m-7-8-030753524.html'),
(224, 80, 2, 6.950000000000000000000000000000, '2025-04-22 10:06:23.901419', 'https://tjomahony.ie/75mm-x-35mm-rough-timber-3-x-1-5-4-8m-15-7-030753548.html'),
(225, 37, 2, 4.500000000000000000000000000000, '2025-04-22 10:06:28.107013', 'https://tjomahony.ie/75mm-x-44mm-rough-timber-3-x-2-2-4m-7-8-030754424.html'),
(226, 77, 2, 4.950000000000000000000000000000, '2025-04-22 10:06:30.431526', 'https://tjomahony.ie/75mm-x-44mm-rough-timber-3-x-2-2-7m-8-9-030754427.html'),
(227, 46, 2, 5.500000000000000000000000000000, '2025-04-22 10:06:32.608455', 'https://tjomahony.ie/75mm-x-44mm-rough-timber-3-x-2-3-0m-9-8-030754430.html'),
(228, 61, 2, 6.500000000000000000000000000000, '2025-04-22 10:06:34.781390', 'https://tjomahony.ie/75mm-x-44mm-rough-timber-3-x-2-3-6m-11-8-030754436.html'),
(229, 17, 2, 8.950000000000000000000000000000, '2025-04-22 10:06:37.097324', 'https://tjomahony.ie/75mm-x-44mm-rough-timber-3-x-2-4-8m-15-7-030754448.html'),
(230, 51, 2, 9.500000000000000000000000000000, '2025-04-22 10:06:39.211320', 'https://tjomahony.ie/75mm-x-44mm-rough-timber-3-x-2-5-4m-17-7-030754454.html'),
(231, 11, 2, 5.950000000000000000000000000000, '2025-04-22 10:06:54.174850', 'https://tjomahony.ie/100mm-x-44mm-rough-timber-c16-en14081-4-x-2-2-4m-7-8-031004424.html'),
(232, 45, 2, 7.500000000000000000000000000000, '2025-04-22 10:06:56.440964', 'https://tjomahony.ie/100mm-x-44mm-rough-timber-c16-en14081-4-x-2-3-0m-9-8-031004430.html'),
(233, 20, 2, 8.950000000000000000000000000000, '2025-04-22 10:06:58.503195', 'https://tjomahony.ie/100mm-x-44mm-rough-timber-c16-en14081-4-x-2-3-6m-11-8-031004436.html'),
(234, 55, 2, 9.950000000000000000000000000000, '2025-04-22 10:07:03.568641', 'https://tjomahony.ie/100mm-x-44mm-rough-timber-c16-en14081-4-x-2-4-2m-13-8-031004442.html'),
(235, 28, 2, 12.500000000000000000000000000000, '2025-04-22 10:07:06.706526', 'https://tjomahony.ie/100mm-x-44mm-rough-timber-c16-en14081-4-x-2-5-4m-17-7-031004454.html'),
(236, 38, 2, 10.500000000000000000000000000000, '2025-04-22 10:07:11.243771', 'https://tjomahony.ie/125mm-x-44mm-rough-timber-c16-en14081-5-x-2-3-6m-11-8-031254436.html'),
(237, 62, 2, 10.500000000000000000000000000000, '2025-04-22 10:07:28.688035', 'https://tjomahony.ie/150mm-x-44mm-rough-timber-c16-en14081-6-x-2-3-0m-9-8-031504430.html'),
(238, 24, 2, 12.500000000000000000000000000000, '2025-04-22 10:07:30.722474', 'https://tjomahony.ie/150mm-x-44mm-rough-timber-c16-en14081-6-x-2-3-6m-11-8-031504436.html'),
(239, 39, 2, 16.500000000000000000000000000000, '2025-04-22 10:07:36.812640', 'https://tjomahony.ie/150mm-x-44mm-rough-timber-c16-en14081-6-x-2-4-8m-15-7-031504448.html'),
(240, 36, 2, 14.500000000000000000000000000000, '2025-04-22 10:07:38.916878', 'https://tjomahony.ie/150mm-x-44mm-rough-timber-c16-en14081-6-x-2-4-2m-13-8-031504442.html'),
(241, 42, 2, 18.500000000000000000000000000000, '2025-04-22 10:07:41.166334', 'https://tjomahony.ie/150mm-x-44mm-rough-timber-c16-en14081-6-x-2-5-4m-17-7-031504454.html'),
(242, 35, 2, 9.950000000000000000000000000000, '2025-04-22 10:07:55.471998', 'https://tjomahony.ie/175mm-x-22mm-rough-timber-7-x-1-4-8m-15-7-031752248.html'),
(243, 69, 2, 32.950000000000000000000000000000, '2025-04-22 10:08:05.335744', 'https://tjomahony.ie/175mm-x-75mm-rough-timber-c16-en14081-7-x-3-4-8m-15-7-031757548.html'),
(244, 30, 2, 12.500000000000000000000000000000, '2025-04-22 10:08:11.310117', 'https://tjomahony.ie/225mm-x-22mm-rough-timber-9-x-1-4-8m-15-7-var-225mmx22mm-rt-c.html'),
(245, 158, 2, 20.950000000000000000000000000000, '2025-04-22 10:08:15.208389', 'https://tjomahony.ie/225mm-x-35mm-rough-timber-c16-en14081-9-x-1-5-4-8m-15-7-var-225mmx35mm-rt-c.html'),
(246, 71, 2, 15.500000000000000000000000000000, '2025-04-22 10:08:19.088331', 'https://tjomahony.ie/225mm-x-44mm-rough-timber-c16-en14081-9-x-2-3-0m-9-8-032254430.html'),
(247, 52, 2, 18.500000000000000000000000000000, '2025-04-22 10:08:21.375889', 'https://tjomahony.ie/225mm-x-44mm-rough-timber-c16-en14081-9-x-2-3-6m-11-8-032254436.html'),
(248, 31, 2, 4.750000000000000000000000000000, '2025-04-22 10:08:39.840921', 'https://tjomahony.ie/50mm-x-35mm-rough-timber-2-x-1-5-4-8m-15-7-030503548.html'),
(249, 23, 2, 5.950000000000000000000000000000, '2025-04-22 10:08:43.629089', 'https://tjomahony.ie/4-8m-50mm-x-44mm-rough-timber-16-2-x-2-030504448.html'),
(250, 79, 2, 8.950000000000000000000000000000, '2025-04-22 10:08:47.459204', 'https://tjomahony.ie/4-8m-100mm-x-35mm-rough-timber-c16-en14081-16-4-x-1-5-031003548.html'),
(251, 32, 2, 19.500000000000000000000000000000, '2025-04-22 10:08:58.020357', 'https://tjomahony.ie/100mm-x-75mm-rough-timber-c16-en14081-4-x-3-4-8m-15-7-031007548.html'),
(252, 177, 2, 3.950000000000000000000000000000, '2025-04-22 10:06:21.428818', 'https://tjomahony.ie/75mm-x-35mm-rough-timber-3-x-1-5-2-7m-8-9-030753527.html'),
(253, 178, 2, 28.950000000000000000000000000000, '2025-04-22 10:07:32.750426', 'https://tjomahony.ie/150mm-x-44mm-rough-timber-c16-en14081-6-x-2-6-6m-21-7-031504466.html'),
(254, 179, 2, 32.950000000000000000000000000000, '2025-04-22 10:07:34.821440', 'https://tjomahony.ie/150mm-x-44mm-rough-timber-c16-en14081-6-x-2-7-2m-23-6-031504472.html'),
(255, 180, 2, 8.950000000000000000000000000000, '2025-04-22 10:07:57.373351', 'https://tjomahony.ie/175mm-x-22mm-rough-timber-7-x-1-4-2m-13-8-031752242.html'),
(256, 181, 2, 15.500000000000000000000000000000, '2025-04-22 10:08:00.746458', 'https://tjomahony.ie/175mm-x-35mm-rough-timber-c16-en14081-7-x-1-5-4-8m-15-7-var-175mmx35mm-rt-c.html'),
(257, 182, 2, 23.950000000000000000000000000000, '2025-04-22 10:08:45.677551', 'https://tjomahony.ie/scaffold-board-2420-x-225-x-63-8-03scaff8.html'),
(258, 183, 2, 22.950000000000000000000000000000, '2025-04-22 10:08:49.291072', 'https://tjomahony.ie/4-8m-75mm-x-75mm-angle-fillet-1-to-give-2-en14081-16-3-x-3-03af757548.html');

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

INSERT INTO `products` (`ProductId`, `Name`, `Description`, `Category`, `Unit`, `CreatedAt`, `UpdatedAt`) VALUES
(6, 'superfoil insulation roll', 'superfoil sf19+ insulation roll - 1.5m x 10m - (covers 15m2)', 'rough timber', '1.5m x 10m', '2025-04-22 09:47:32.951408', '2025-04-22 09:47:32.951278'),
(7, 'gyproc metal stud s', 'gyproc metal stud 3600mm 70 s 50', 'rough timber', '50mm x 70mm x 3600mm', '2025-04-22 09:47:32.957019', '2025-04-22 09:47:32.957019'),
(8, 'kilsaran kpro crete 3:1 sand  cement mix 25kg', 'kilsaran kpro crete 3:1 - sand &amp; cement mix 25kg', 'rough timber', '', '2025-04-22 09:47:32.957257', '2025-04-22 09:47:32.957257'),
(9, 'kilsaran cashel natural paving slabs', 'kilsaran cashel natural paving slabs - 400mm x 400mm x 40mm', 'rough timber', '40mm x 400mm x 400mm', '2025-04-22 09:47:32.957327', '2025-04-22 09:47:32.957327'),
(10, 'treated rough white deal timber', 'treated rough white deal timber 100mm x 44mm x 4.8m (4 x 2 inches approx)', 'rough timber', '44mm x 100mm x 4.8m', '2025-04-22 09:47:32.957386', '2025-04-22 09:47:32.957386'),
(11, 'white deal rough timber', 'white deal rough timber 100mm x 44mm x 2.4m (4 inches x 2 inches)', 'rough timber', '44mm x 100mm x 2.4m', '2025-04-22 09:47:32.957443', '2025-04-22 09:47:32.957443'),
(12, 'treated rough white deal timber', 'treated rough white deal timber 150mm x 44mm x 4.8m (6 x 2 inches approx)', 'rough timber', '44mm x 150mm x 4.8m', '2025-04-22 09:47:32.957548', '2025-04-22 09:47:32.957547'),
(13, 'treated rough white deal timber', 'treated rough white deal timber 100mm x 22mm x 4.8m (4 x 1 inches approx)', 'rough timber', '22mm x 100mm x 4.8m', '2025-04-22 09:47:32.957620', '2025-04-22 09:47:32.957620'),
(14, 'white deal rough timber', 'white deal rough timber 50mm x 22mm x 4.8m (2 inches x 1 inches)', 'rough timber', '22mm x 50mm x 4.8m', '2025-04-22 09:47:32.957718', '2025-04-22 09:47:32.957718'),
(15, 'treated rough white deal timber', 'treated rough white deal timber 150mm x 22mm x 4.8m (6 x 1 inches).', 'rough timber', '22mm x 150mm x 4.8m', '2025-04-22 09:47:32.957801', '2025-04-22 09:47:32.957801'),
(16, 'treated rough white deal timber', 'treated rough white deal timber - 50mm x 35mm x 4.8m (2 x 1.5 inches approx)', 'rough timber', '35mm x 50mm x 4.8m', '2025-04-22 09:47:32.957899', '2025-04-22 09:47:32.957899'),
(17, 'white deal rough timber', 'white deal rough timber 75mm x 44mm x 4.8m (3 inches x 2 inches)', 'rough timber', '44mm x 75mm x 4.8m', '2025-04-22 09:47:32.957965', '2025-04-22 09:47:32.957965'),
(18, 'treated rough white deal timber 50mmx', 'treated rough white deal timber 50mmx 44mm x 4.8m (2 x 2 inches approx)', 'rough timber', '44mm x 4.8m', '2025-04-22 09:47:32.958076', '2025-04-22 09:47:32.958076'),
(19, 'white deal rough timber', 'white deal rough timber - 100mm x 44mm x 4.8m ( 4 x 2 inches approx)', 'rough timber', '44mm x 100mm x 4.8m', '2025-04-22 09:47:32.958174', '2025-04-22 09:47:32.958173'),
(20, 'white deal rough timber', 'white deal rough timber 100mm x 44mm x 3.6m (4 inches x 2 inches)', 'rough timber', '44mm x 100mm x 3.6m', '2025-04-22 09:47:32.958243', '2025-04-22 09:47:32.958243'),
(21, 'treated rough white deal timber', 'treated rough white deal timber - 100mm x 75mm x 4.8m (4 x 3 inches approx) )', 'rough timber', '75mm x 100mm x 4.8m', '2025-04-22 09:47:32.958294', '2025-04-22 09:47:32.958294'),
(22, 'treated rough white deal timber', 'treated rough white deal timber - 175mm x 22mm x 4.8m ( 7 x 1 inches approx.)', 'rough timber', '22mm x 175mm x 4.8m', '2025-04-22 09:47:33.891765', '2025-04-22 09:47:33.891765'),
(23, 'white deal rough timber', 'white deal rough timber - 50mm x 44mm x 4.8m ( 2.5 x 2 inches approx)', 'rough timber', '44mm x 50mm x 4.8m', '2025-04-22 09:47:33.892044', '2025-04-22 09:47:33.892044'),
(24, 'white deal rough timber', 'white deal rough timber - 150mm x 44mm x 3.6m ( 6 x 2 inches approx)', 'rough timber', '44mm x 150mm x 3.6m', '2025-04-22 09:47:33.892097', '2025-04-22 09:47:33.892097'),
(25, 'white deal rough timber', 'white deal rough timber - 100mm x 22mm x 4.8m ( 4 x 1 inches approx)', 'rough timber', '22mm x 100mm x 4.8m', '2025-04-22 09:47:33.892144', '2025-04-22 09:47:33.892144'),
(26, 'treated rough white deal timber', 'treated rough white deal timber - 150mm x 75mm x 4.8m ( 6 x 3 inches approx )', 'rough timber', '75mm x 150mm x 4.8m', '2025-04-22 09:47:33.892184', '2025-04-22 09:47:33.892184'),
(27, 'treated rough white deal timber', 'treated rough white deal timber - 225mm x 22mm x 4.8m ( 9 x 1 inches approx.)', 'rough timber', '22mm x 225mm x 4.8m', '2025-04-22 09:47:33.892241', '2025-04-22 09:47:33.892241'),
(28, 'white deal rough timber', 'white deal rough timber - 100mm x 44mm x 5.4m ( 4 x 2 inches approx)', 'rough timber', '44mm x 100mm x 5.4m', '2025-04-22 09:47:33.892289', '2025-04-22 09:47:33.892288'),
(29, 'white deal rough timber', 'white deal rough timber - 125mm x 44mm x 4.8m  ( 5 x 2 inches approx)', 'rough timber', '44mm x 125mm x 4.8m', '2025-04-22 09:47:33.892329', '2025-04-22 09:47:33.892329'),
(30, 'white deal rough timber', 'white deal rough timber - 225mm x 22mm x 4.8m ( 9 x  1 inches approx)', 'rough timber', '22mm x 225mm x 4.8m', '2025-04-22 09:47:33.892378', '2025-04-22 09:47:33.892378'),
(31, 'white deal rough timber', 'white deal rough timber - 50mm x 35mm x 4.8m ( 2 x 1.5 inches approx)', 'rough timber', '35mm x 50mm x 4.8m', '2025-04-22 09:47:33.892571', '2025-04-22 09:47:33.892570'),
(32, 'white deal rough timber', 'white deal rough timber - 100mm x 75mm x 4.8m ( 4 x 3 inches approx)', 'rough timber', '75mm x 100mm x 4.8m', '2025-04-22 09:47:33.892624', '2025-04-22 09:47:33.892624'),
(33, 'treated rough white deal timber', 'treated rough white deal timber - 75mm x 44mm x 4.8m', 'rough timber', '44mm x 75mm x 4.8m', '2025-04-22 09:47:33.892667', '2025-04-22 09:47:33.892667'),
(34, 'white deal rough timber', 'white deal rough timber 50mm x 22mm x 4.8m (2 x 1 inches approx)', 'rough timber', '22mm x 50mm x 4.8m', '2025-04-22 09:47:35.071051', '2025-04-22 09:47:35.071051'),
(35, 'white deal rough timber', 'white deal rough timber - 175mm x 22mm x 4.8m ( 7 x 1 inches approx)', 'rough timber', '22mm x 175mm x 4.8m', '2025-04-22 09:47:35.071255', '2025-04-22 09:47:35.071255'),
(36, 'white deal rough timber', 'white deal rough timber - 150mm x 44mm x 4.2m', 'rough timber', '44mm x 150mm x 4.2m', '2025-04-22 09:47:35.071301', '2025-04-22 09:47:35.071301'),
(37, 'white deal rough timber', 'white deal rough timber - 75mm x 44mm x 2.4m ( 3 x 2 inches approx)', 'rough timber', '44mm x 75mm x 2.4m', '2025-04-22 09:47:35.071362', '2025-04-22 09:47:35.071362'),
(38, 'white deal rough  timber', 'white deal rough  timber - 125mm x 44mm x 3.6m ( 5 x 2 inches approx)', 'rough timber', '44mm x 125mm x 3.6m', '2025-04-22 09:47:35.071398', '2025-04-22 09:47:35.071398'),
(39, 'white deal rough timber', 'white deal rough timber - 150mm x 44mm x 4.8m ( 6 x 2 inches approx)', 'rough timber', '44mm x 150mm x 4.8m', '2025-04-22 09:47:35.071439', '2025-04-22 09:47:35.071439'),
(40, 'treated rough white deal timber', 'treated rough white deal timber - 225mm x 44mm x 4.8m', 'rough timber', '44mm x 225mm x 4.8m', '2025-04-22 09:47:35.071474', '2025-04-22 09:47:35.071474'),
(41, 'white deal rough timber', 'white deal rough timber - 225mm x 44mm x 5.4m ( 9 x 2 inches approx)', 'rough timber', '44mm x 225mm x 5.4m', '2025-04-22 09:47:35.071506', '2025-04-22 09:47:35.071506'),
(42, 'white deal rough timber', 'white deal rough timber - 150mm x 44mm x 5.4m ( 6 x 2 inches approx)', 'rough timber', '44mm x 150mm x 5.4m', '2025-04-22 09:47:35.071539', '2025-04-22 09:47:35.071539'),
(43, 'white deal rough timber', 'white deal rough timber - 125mm x 44mm x 5.4m ( 5 x 2 inches approx)', 'rough timber', '44mm x 125mm x 5.4m', '2025-04-22 09:47:35.071571', '2025-04-22 09:47:35.071571'),
(44, 'rough treated timber white deal', 'rough treated timber - white deal - 50mm x 22mm x 4.5m', 'rough timber', '22mm x 50mm x 4.5m', '2025-04-22 09:47:35.071615', '2025-04-22 09:47:35.071615'),
(45, 'white deal rough timber', 'white deal rough timber - 100mm x 44mm x 3.0m ( 4 x 2 inches)', 'rough timber', '44mm x 100mm x 3.0m', '2025-04-22 09:47:35.071648', '2025-04-22 09:47:35.071648'),
(46, 'white deal rough timber', 'white deal rough timber - 75mm x 44mm x 3.0m ( 3 x 2 inches )', 'rough timber', '44mm x 75mm x 3.0m', '2025-04-22 09:47:35.834308', '2025-04-22 09:47:35.834307'),
(47, 'white deal rough timber', 'white deal rough timber - 225mm x 44mm x 4.8m ( 9 x 2 inches)', 'rough timber', '44mm x 225mm x 4.8m', '2025-04-22 09:47:35.834461', '2025-04-22 09:47:35.834461'),
(48, 'white deal rough timber', 'white deal rough timber - 150mm x 44mm x 6.0m ( 6 x 2 inches)', 'rough timber', '44mm x 150mm x 6.0m', '2025-04-22 09:47:35.834497', '2025-04-22 09:47:35.834497'),
(49, 'white deal rough timber', 'white deal rough timber - 225mm x 75mm x 4.8m ( 9 x 3 inches)', 'rough timber', '75mm x 225mm x 4.8m', '2025-04-22 09:47:35.834520', '2025-04-22 09:47:35.834520'),
(50, 'white deal rough treated timber', 'white deal rough treated timber - 100mm x 35mm x 4.8m ( 4 x 1 inches)', 'rough timber', '35mm x 100mm x 4.8m', '2025-04-22 09:47:35.834543', '2025-04-22 09:47:35.834543'),
(51, 'white deal rough timber', 'white deal rough timber - 75mm x 44mm x 5.4m ( 3 x 2 inches)', 'rough timber', '44mm x 75mm x 5.4m', '2025-04-22 09:47:35.834576', '2025-04-22 09:47:35.834576'),
(52, 'white deal rough timber', 'white deal rough timber - 225mm x 44mm x 3.6m ( 9 x 2 inches )', 'rough timber', '44mm x 225mm x 3.6m', '2025-04-22 09:47:35.834611', '2025-04-22 09:47:35.834611'),
(53, 'white deal rough treated timber', 'white deal rough treated timber - 100mm x 44mm x 5.1m ( 4 x 2 inches)', 'rough timber', '44mm x 100mm x 5.1m', '2025-04-22 09:47:35.834632', '2025-04-22 09:47:35.834632'),
(54, 'white deal rough timber', 'white deal rough timber - 225mm x 44mm x 4.2m ( 9 x 2 inches)', 'rough timber', '44mm x 225mm x 4.2m', '2025-04-22 09:47:35.834655', '2025-04-22 09:47:35.834655'),
(55, 'white deal rough timber', 'white deal rough timber - 100mm x 44mm x 4.2m ( 4 x 2 inches )', 'rough timber', '44mm x 100mm x 4.2m', '2025-04-22 09:47:35.834678', '2025-04-22 09:47:35.834677'),
(56, 'white deal rough treated timber', 'white deal rough treated timber -175mm x 44mm x 4.8m  ( 7 x 2 inches)', 'rough timber', '44mm x 175mm x 4.8m', '2025-04-22 09:47:35.834701', '2025-04-22 09:47:35.834701'),
(57, 'white deal rough timber', 'white deal rough timber - 175mm x 44mm x 5.4m ( 7 x 2 inches)', 'rough timber', '44mm x 175mm x 5.4m', '2025-04-22 09:47:35.834728', '2025-04-22 09:47:35.834728'),
(58, 'white deal rough timber', 'white deal rough timber - 75mm x 35mm x 2.4m ( 3 x 1 inches)', 'rough timber', '35mm x 75mm x 2.4m', '2025-04-22 09:47:37.177529', '2025-04-22 09:47:37.177528'),
(59, 'white deal rough timber', 'white deal rough timber - 175mm x 44mm x 3.6m ( 7 x 2 inches)', 'rough timber', '44mm x 175mm x 3.6m', '2025-04-22 09:47:37.177636', '2025-04-22 09:47:37.177636'),
(60, 'white deal rough timber', 'white deal rough timber - 175mm x 44mm x  4.2m ( 7 x 2 inches)', 'rough timber', '44mm x 175mm x 4.2m', '2025-04-22 09:47:37.177673', '2025-04-22 09:47:37.177673'),
(61, 'white deal rough timber', 'white deal rough timber - 75mm x 44mm x 3.6m ( 3 x 2 inches)', 'rough timber', '44mm x 75mm x 3.6m', '2025-04-22 09:47:37.177803', '2025-04-22 09:47:37.177803'),
(62, 'white deal rough timber', 'white deal rough timber - 150mm x 44mm x 3.0m ( 6 x 2 inches)', 'rough timber', '44mm x 150mm x 3.0m', '2025-04-22 09:47:37.177880', '2025-04-22 09:47:37.177880'),
(63, 'furring piece rough timber', 'furring piece rough timber - 75mm x 44mm x 4.8m ( 3 x 2 inches)  ( diagonal cut . 2 x 4.8m pieces)', 'rough timber', '44mm x 75mm x 4.8m', '2025-04-22 09:47:37.177952', '2025-04-22 09:47:37.177952'),
(64, 'furring piece rough timber', 'furring piece rough timber - 100mm x 44mm x 4.8m ( 4 x 2 inches)  (diagonal cut . 2 x 4.8m pieces)', 'rough timber', '44mm x 100mm x 4.8m', '2025-04-22 09:47:37.178055', '2025-04-22 09:47:37.178055'),
(65, 'white deal rough treated timber', 'white deal rough treated timber - 100mm x 44mm x 2.4m ( 4 x 2 inches)', 'rough timber', '44mm x 100mm x 2.4m', '2025-04-22 09:47:37.178134', '2025-04-22 09:47:37.178134'),
(66, 'white deal rough treated timber', 'white deal rough treated timber - 100mm x 22mm x 4.5m ( 4 x 1 inches )', 'rough timber', '22mm x 100mm x 4.5m', '2025-04-22 09:47:37.178209', '2025-04-22 09:47:37.178209'),
(67, 'white deal rough treated timber', 'white deal rough treated timber - 225mm x 75mm x 4.8m (9 x 3 inches )', 'rough timber', '75mm x 225mm x 4.8m', '2025-04-22 09:47:37.178283', '2025-04-22 09:47:37.178283'),
(68, 'white deal rough timber', 'white deal rough timber - 225mm x 75mm x 5.4m ( 9 x 3 inches)', 'rough timber', '75mm x 225mm x 5.4m', '2025-04-22 09:47:37.178364', '2025-04-22 09:47:37.178364'),
(69, 'white deal rough timber', 'white deal rough timber - 175mm x 75mm x 4.8m ( 7 x 3 inches)', 'rough timber', '75mm x 175mm x 4.8m', '2025-04-22 09:47:37.178438', '2025-04-22 09:47:37.178438'),
(70, 'white deal rough timber', 'white deal rough timber - 150mm x 75mm x 4.8m ( 6 x 3 inches)', 'rough timber', '75mm x 150mm x 4.8m', '2025-04-22 09:47:38.911745', '2025-04-22 09:47:38.911744'),
(71, 'white deal rough timber', 'white deal rough timber - 225mm x 44mm x 3.0m ( 9 x 2 inches)', 'rough timber', '44mm x 225mm x 3.0m', '2025-04-22 09:47:38.911849', '2025-04-22 09:47:38.911848'),
(72, 'white deal rough timber', 'white deal rough timber - 175mm x 44mm x 6.0m ( 7 x 2 inches)', 'rough timber', '44mm x 175mm x 6.0m', '2025-04-22 09:47:38.911871', '2025-04-22 09:47:38.911871'),
(73, 'white deal rough timber', 'white deal rough timber - 175mm x 44mm x 4.8m ( 7 x 2 inches)', 'rough timber', '44mm x 175mm x 4.8m', '2025-04-22 09:47:38.911890', '2025-04-22 09:47:38.911890'),
(74, 'white deal rough timber', 'white deal rough timber - 175mm x 44mm x 3.0m ( 7 x 2 inches)', 'rough timber', '44mm x 175mm x 3.0m', '2025-04-22 09:47:38.911907', '2025-04-22 09:47:38.911907'),
(75, 'white deal rough timber', 'white deal rough timber - 125mm x 44mm x  4.2m ( 5 x 2 inches)', 'rough timber', '44mm x 125mm x 4.2m', '2025-04-22 09:47:38.911925', '2025-04-22 09:47:38.911925'),
(76, 'white deal rough timber', 'white deal rough timber - 75mm x 44mm x 4.2m ( 3 x 2 inches)', 'rough timber', '44mm x 75mm x 4.2m', '2025-04-22 09:47:38.911959', '2025-04-22 09:47:38.911959'),
(77, 'white deal rough timber', 'white deal rough timber - 75mm x 44mm x 2.7m ( 3 x 2 inches approx)', 'rough timber', '44mm x 75mm x 2.7m', '2025-04-22 09:47:38.911977', '2025-04-22 09:47:38.911977'),
(78, 'white deal rough timber', 'white deal rough timber - 115mm x 35mm x 4.8m ( 5 x 1 inches)', 'rough timber', '35mm x 115mm x 4.8m', '2025-04-22 09:47:38.911998', '2025-04-22 09:47:38.911998'),
(79, 'white deal rough timber', 'white deal rough timber - 100mm x 35mm x 4.8m ( 4 x 1 inches)', 'rough timber', '35mm x 100mm x 4.8m', '2025-04-22 09:47:38.912016', '2025-04-22 09:47:38.912016'),
(80, 'white deal rough timber', 'white deal rough timber - 75mm x 35mm x 4.8m ( 3 x 1 inches)', 'rough timber', '35mm x 75mm x 4.8m', '2025-04-22 09:47:38.912036', '2025-04-22 09:47:38.912036'),
(81, 'white deal rough timber', 'white deal rough timber - 50mm x 22mm x 4.5m ( 2 x 1 inches )', 'rough timber', '22mm x 50mm x 4.5m', '2025-04-22 09:47:38.912058', '2025-04-22 09:47:38.912058'),
(82, 'imported rough white deal', '2.7m 75 x 44 imported rough white deal', 'rough timber', '44mm x 75mm x 2.7m', '2025-04-22 09:47:40.043963', '2025-04-22 09:47:40.043963'),
(83, 'imported white deal rough wdr', '100 x 35 imported white deal rough wdr', 'rough timber', '35mm x 100mm', '2025-04-22 09:47:40.044065', '2025-04-22 09:47:40.044065'),
(84, 'imported rough white deal treated', '150 x 44 imported rough white deal treated', 'rough timber', '44mm x 150mm', '2025-04-22 09:47:40.044094', '2025-04-22 09:47:40.044094'),
(85, 'imported rough white deal treated', '5.4m 150 x 44 imported rough white deal treated', 'rough timber', '44mm x 150mm x 5.4m', '2025-04-22 09:47:40.044130', '2025-04-22 09:47:40.044130'),
(86, 'imported rough white deal', '4.8m 50 x 35 imported rough white deal', 'rough timber', '35mm x 50mm x 4.8m', '2025-04-22 09:47:40.044149', '2025-04-22 09:47:40.044149'),
(87, 'imported rough white deal treated', '4.8m 100 x 75 imported rough white deal treated', 'rough timber', '75mm x 100mm x 4.8m', '2025-04-22 09:47:40.044171', '2025-04-22 09:47:40.044171'),
(88, 'imported rough white deal treated', '100 x 75 imported rough white deal treated', 'rough timber', '75mm x 100mm', '2025-04-22 09:47:40.044199', '2025-04-22 09:47:40.044199'),
(89, 'imported white deal rough', '5.1m 50 x 36 imported white deal rough sr82', 'rough timber', '36mm x 50mm x 5.1m', '2025-04-22 09:47:40.044224', '2025-04-22 09:47:40.044224'),
(90, 'imported rough white deal treated', '4.8m 100 x 35 imported rough white deal treated', 'rough timber', '35mm x 100mm x 4.8m', '2025-04-22 09:47:40.044241', '2025-04-22 09:47:40.044241'),
(91, 'imported rough white deal treated', '100 x 35 imported rough white deal treated', 'rough timber', '35mm x 100mm', '2025-04-22 09:47:40.044259', '2025-04-22 09:47:40.044259'),
(92, 'imported rough white deal treated', '4.2m 50 x 44 imported rough white deal treated', 'rough timber', '44mm x 50mm x 4.2m', '2025-04-22 09:47:40.044279', '2025-04-22 09:47:40.044279'),
(93, 'imported rough white deal treated', '50 x 44 imported rough white deal treated', 'rough timber', '44mm x 50mm', '2025-04-22 09:47:40.044298', '2025-04-22 09:47:40.044298'),
(94, 'imported rough white deal', '5.1m 100 x 44 imported rough white deal', 'rough timber', '44mm x 100mm x 5.1m', '2025-04-22 09:47:42.439479', '2025-04-22 09:47:42.439478'),
(95, 'imported rough white deal', '5.1m 175 x 44 imported rough white deal', 'rough timber', '44mm x 175mm x 5.1m', '2025-04-22 09:47:42.439576', '2025-04-22 09:47:42.439576'),
(96, 'imported rough white deal', '3.3m 150 x 44 imported rough white deal', 'rough timber', '44mm x 150mm x 3.3m', '2025-04-22 09:47:42.439598', '2025-04-22 09:47:42.439598'),
(97, 'truss timber rough white deal', '5.1m 225 x 44 truss timber c24 rough white deal', 'rough timber', '44mm x 225mm x 5.1m', '2025-04-22 09:47:42.439616', '2025-04-22 09:47:42.439616'),
(98, 'truss timber rough white deal', '3.6m 225 x 44 truss timber c24 rough white deal', 'rough timber', '44mm x 225mm x 3.6m', '2025-04-22 09:47:42.439634', '2025-04-22 09:47:42.439634'),
(99, 'imported rough white deal', '4.2m 150 x 35 imported rough white deal', 'rough timber', '35mm x 150mm x 4.2m', '2025-04-22 09:47:42.439652', '2025-04-22 09:47:42.439652'),
(100, 'imported rough white deal', '5.7m 225 x 75 imported rough white deal', 'rough timber', '75mm x 225mm x 5.7m', '2025-04-22 09:47:42.439668', '2025-04-22 09:47:42.439668'),
(101, 'imported rough white deal', '5.1m 225 x 75 imported rough white deal', 'rough timber', '75mm x 225mm x 5.1m', '2025-04-22 09:47:42.439685', '2025-04-22 09:47:42.439685'),
(102, 'imported rough white deal', '4.5m 225 x 75 imported rough white deal', 'rough timber', '75mm x 225mm x 4.5m', '2025-04-22 09:47:42.439701', '2025-04-22 09:47:42.439700'),
(103, 'imported rough white deal', '4.2m 225 x 75 imported rough white deal', 'rough timber', '75mm x 225mm x 4.2m', '2025-04-22 09:47:42.439716', '2025-04-22 09:47:42.439716'),
(104, 'imported rough white deal', '3.0m 225 x 75 imported rough white deal', 'rough timber', '75mm x 225mm x 3.0m', '2025-04-22 09:47:42.439732', '2025-04-22 09:47:42.439732'),
(105, 'imported rough white deal', '3.9m 175 x 44 imported rough white deal', 'rough timber', '44mm x 175mm x 3.9m', '2025-04-22 09:47:42.439748', '2025-04-22 09:47:42.439748'),
(106, 'imported rough white deal', '4.2m 125 x 44 imported rough white deal', 'rough timber', '44mm x 125mm x 4.2m', '2025-04-22 09:47:43.830739', '2025-04-22 09:47:43.830739'),
(107, 'imported wh deal rough treated', '4.2m 50 x 36 imported wh deal rough sr82 treated (wc)', 'rough timber', '36mm x 50mm x 4.2m', '2025-04-22 09:47:43.830870', '2025-04-22 09:47:43.830870'),
(108, 'imported rough white deal', '3.3m 175 x 44 imported rough white deal', 'rough timber', '44mm x 175mm x 3.3m', '2025-04-22 09:47:43.830898', '2025-04-22 09:47:43.830898'),
(109, 'imported rough white deal', '4.2m 100 x 44 imported rough white deal', 'rough timber', '44mm x 100mm x 4.2m', '2025-04-22 09:47:43.830916', '2025-04-22 09:47:43.830915'),
(110, 'imported rough white deal', '2.4m 100 x 44 imported rough white deal', 'rough timber', '44mm x 100mm x 2.4m', '2025-04-22 09:47:43.830935', '2025-04-22 09:47:43.830934'),
(111, 'imported rough white deal', '4.5m 175 x 44 imported rough white deal', 'rough timber', '44mm x 175mm x 4.5m', '2025-04-22 09:47:43.830952', '2025-04-22 09:47:43.830952'),
(112, 'imported rough white deal', '3.6m 150 x 44 imported rough white deal', 'rough timber', '44mm x 150mm x 3.6m', '2025-04-22 09:47:43.830970', '2025-04-22 09:47:43.830970'),
(113, 'imported rough white deal', '4.2m 75 x 44 imported rough white deal', 'rough timber', '44mm x 75mm x 4.2m', '2025-04-22 09:47:43.830988', '2025-04-22 09:47:43.830988'),
(114, 'imported rough white deal', '4.5m 150 x 44 imported rough white deal', 'rough timber', '44mm x 150mm x 4.5m', '2025-04-22 09:47:43.831005', '2025-04-22 09:47:43.831005'),
(115, 'imported rough white deal', '3.0m 150 x 44 imported rough white deal', 'rough timber', '44mm x 150mm x 3.0m', '2025-04-22 09:47:43.831024', '2025-04-22 09:47:43.831024'),
(116, 'imported rough white deal', '3.9m 125 x 44 imported rough white deal', 'rough timber', '44mm x 125mm x 3.9m', '2025-04-22 09:47:43.831041', '2025-04-22 09:47:43.831041'),
(117, 'imported rough white deal', '4.2m 50 x 44 imported rough white deal', 'rough timber', '44mm x 50mm x 4.2m', '2025-04-22 09:47:43.831060', '2025-04-22 09:47:43.831060'),
(118, 'imported rough white deal', '4.5m 225 x 44 imported rough white deal', 'rough timber', '44mm x 225mm x 4.5m', '2025-04-22 09:47:44.926524', '2025-04-22 09:47:44.926523'),
(119, 'imported rough white deal', '5.1m 150 x 44 imported rough white deal', 'rough timber', '44mm x 150mm x 5.1m', '2025-04-22 09:47:44.926597', '2025-04-22 09:47:44.926597'),
(120, 'imported rough white deal', '3.6m 125 x 44 imported rough white deal', 'rough timber', '44mm x 125mm x 3.6m', '2025-04-22 09:47:44.926618', '2025-04-22 09:47:44.926618'),
(121, 'imported white deal rough treated', '50 x 36 imported white deal rough sr82 treated', 'rough timber', '36mm x 50mm', '2025-04-22 09:47:44.926635', '2025-04-22 09:47:44.926635'),
(122, 'imported white deal rough', '50 x 36 imported white deal rough sr82', 'rough timber', '36mm x 50mm', '2025-04-22 09:47:44.926655', '2025-04-22 09:47:44.926655'),
(123, 'truss timber rough white deal', '7.8m 225 x 44 truss timber c24 rough white deal', 'rough timber', '44mm x 225mm x 7.8m', '2025-04-22 09:47:44.926671', '2025-04-22 09:47:44.926671'),
(124, 'imported rough white deal', '4.8m 150 x 22 imported rough white deal', 'rough timber', '22mm x 150mm x 4.8m', '2025-04-22 09:47:44.926689', '2025-04-22 09:47:44.926689'),
(125, 'truss timber rough white deal', '7.2m 225 x 44 truss timber c24 rough white deal', 'rough timber', '44mm x 225mm x 7.2m', '2025-04-22 09:47:44.926705', '2025-04-22 09:47:44.926705'),
(126, 'imported rough white deal', '4.8m 175 x 22 imported rough white deal', 'rough timber', '22mm x 175mm x 4.8m', '2025-04-22 09:47:44.926721', '2025-04-22 09:47:44.926721'),
(127, 'imported rough white deal treated', '4.8m 100 x 22 imported rough white deal treated', 'rough timber', '22mm x 100mm x 4.8m', '2025-04-22 09:47:44.926740', '2025-04-22 09:47:44.926740'),
(128, 'imported rough white deal', '3.6m 175 x 44 imported rough white deal', 'rough timber', '44mm x 175mm x 3.6m', '2025-04-22 09:47:44.926758', '2025-04-22 09:47:44.926757'),
(129, 'imported wh deal rough treated', '4.8m 50 x 36 imported wh deal rough sr82 treated', 'rough timber', '36mm x 50mm x 4.8m', '2025-04-22 09:47:44.926773', '2025-04-22 09:47:44.926773'),
(130, 'imported white deal rough', '4.8m 50 x 36 imported white deal rough sr82', 'rough timber', '36mm x 50mm x 4.8m', '2025-04-22 09:47:46.134809', '2025-04-22 09:47:46.134808'),
(131, 'imported white deal rough', '4.5m 50 x 36 imported white deal rough sr82', 'rough timber', '36mm x 50mm x 4.5m', '2025-04-22 09:47:46.134923', '2025-04-22 09:47:46.134923'),
(132, 'imported white deal rough', '4.2m 50 x 36 imported white deal rough sr82', 'rough timber', '36mm x 50mm x 4.2m', '2025-04-22 09:47:46.134947', '2025-04-22 09:47:46.134947'),
(133, 'imported rough white deal', '4.8m 225 x 22 imported rough white deal', 'rough timber', '22mm x 225mm x 4.8m', '2025-04-22 09:47:46.134968', '2025-04-22 09:47:46.134968'),
(134, 'imported rough white deal', '4.8m 225 x 35 imported rough white deal', 'rough timber', '35mm x 225mm x 4.8m', '2025-04-22 09:47:46.134991', '2025-04-22 09:47:46.134990'),
(135, 'imported rough white deal', '225 x 35 imported rough white deal', 'rough timber', '35mm x 225mm', '2025-04-22 09:47:46.135011', '2025-04-22 09:47:46.135011'),
(136, 'imported rough white deal', '5.7m 225 x 44 imported rough white deal', 'rough timber', '44mm x 225mm x 5.7m', '2025-04-22 09:47:46.135036', '2025-04-22 09:47:46.135036'),
(137, 'imported rough white deal', '5.1m 225 x 44 imported rough white deal', 'rough timber', '44mm x 225mm x 5.1m', '2025-04-22 09:47:46.135059', '2025-04-22 09:47:46.135059'),
(138, 'imported rough white deal', '3.9m 225 x 44 imported rough white deal', 'rough timber', '44mm x 225mm x 3.9m', '2025-04-22 09:47:46.135079', '2025-04-22 09:47:46.135079'),
(139, 'truss timber rough white deal', '6.0m 175 x 44 truss timber c24 rough white deal', 'rough timber', '44mm x 175mm x 6.0m', '2025-04-22 09:47:46.135099', '2025-04-22 09:47:46.135099'),
(140, 'imported rough white deal', '3.3m 225 x 44 imported rough white deal', 'rough timber', '44mm x 225mm x 3.3m', '2025-04-22 09:47:46.135120', '2025-04-22 09:47:46.135120'),
(141, 'imported rough white deal', '3.0m 225 x 44 imported rough white deal', 'rough timber', '44mm x 225mm x 3.0m', '2025-04-22 09:47:46.135141', '2025-04-22 09:47:46.135140'),
(142, 'imported rough white deal', '4.8m 125 x 44 imported rough white deal', 'rough timber', '44mm x 125mm x 4.8m', '2025-04-22 09:47:48.356857', '2025-04-22 09:47:48.356857'),
(143, 'imported rough white deal', '4.5m 125 x 44 imported rough white deal', 'rough timber', '44mm x 125mm x 4.5m', '2025-04-22 09:47:48.356941', '2025-04-22 09:47:48.356941'),
(144, 'imported rough white deal', '5.4m 150 x 44 imported rough white deal', 'rough timber', '44mm x 150mm x 5.4m', '2025-04-22 09:47:48.356961', '2025-04-22 09:47:48.356961'),
(145, 'imported rough white deal', '4.2m 150 x 44 imported rough white deal', 'rough timber', '44mm x 150mm x 4.2m', '2025-04-22 09:47:48.356979', '2025-04-22 09:47:48.356979'),
(146, 'imported rough white deal', '3.9m 150 x 44 imported rough white deal', 'rough timber', '44mm x 150mm x 3.9m', '2025-04-22 09:47:48.356995', '2025-04-22 09:47:48.356995'),
(147, 'truss timber rough white deal', '6.0m 225 x 44 truss timber c24 rough white deal', 'rough timber', '44mm x 225mm x 6.0m', '2025-04-22 09:47:48.357011', '2025-04-22 09:47:48.357010'),
(148, 'imported rough white deal', '4.2m 175 x 44 imported rough white deal', 'rough timber', '44mm x 175mm x 4.2m', '2025-04-22 09:47:48.357028', '2025-04-22 09:47:48.357028'),
(149, 'imported rough white deal', '225 x 22 imported rough white deal', 'rough timber', '22mm x 225mm', '2025-04-22 09:47:48.357044', '2025-04-22 09:47:48.357044'),
(150, 'imported rough white deal', '175 x 22 imported rough white deal', 'rough timber', '22mm x 175mm', '2025-04-22 09:47:48.357061', '2025-04-22 09:47:48.357061'),
(151, 'imported rough white deal', '150 x 22 imported rough white deal', 'rough timber', '22mm x 150mm', '2025-04-22 09:47:48.357076', '2025-04-22 09:47:48.357076'),
(152, 'imported rough white deal', '75 x 44 imported rough white deal', 'rough timber', '44mm x 75mm', '2025-04-22 09:47:48.357092', '2025-04-22 09:47:48.357092'),
(153, 'imported rough white deal', '4.8m 100 x 75 imported rough white deal', 'rough timber', '75mm x 100mm x 4.8m', '2025-04-22 09:47:48.357107', '2025-04-22 09:47:48.357107'),
(154, 'steico i joist timber beams', 'steico i-joist timber beams 45mm x 300mm x 13m', 'rough timber', '45mm x 300mm x 13m', '2025-04-22 09:47:49.623064', '2025-04-22 09:47:49.623064'),
(155, 'steico i joist timber beams', 'steico i-joist timber beams 45mm x 240mm x 13m', 'rough timber', '45mm x 240mm x 13m', '2025-04-22 09:47:49.623150', '2025-04-22 09:47:49.623150'),
(156, 'rough white deal', '6.6m 225 x 44 rough white deal', 'rough timber', '44mm x 225mm x 6.6m', '2025-04-22 09:47:49.623171', '2025-04-22 09:47:49.623171'),
(157, 'rough white deal', '6.0m 225 x 44 rough white deal', 'rough timber', '44mm x 225mm x 6.0m', '2025-04-22 09:47:49.623190', '2025-04-22 09:47:49.623190'),
(158, 'white deal rough timber', 'white deal rough timber - 225mm x 35mm x 4.8m ( 9 x 1.5 inches approx)', 'rough timber', '35mm x 225mm x 4.8m', '2025-04-22 09:47:49.623206', '2025-04-22 09:47:49.623205'),
(159, 'white deal rough treated timber', 'white deal rough treated timber - 225mm x 35mm x 4.8m (9 x 2 inches)', 'rough timber', '35mm x 225mm x 4.8m', '2025-04-22 09:47:49.623225', '2025-04-22 09:47:49.623225'),
(160, 'white deal rough treated timber', 'white deal rough treated timber - 175mm x 35mm x 4.8m (7 x 1 inches)', 'rough timber', '35mm x 175mm x 4.8m', '2025-04-22 09:47:49.623242', '2025-04-22 09:47:49.623242'),
(161, 'white deal rough timber', 'white deal rough timber - 50mm x 44mm x 4.5m ( 2 x 2 inches)', 'rough timber', '44mm x 50mm x 4.5m', '2025-04-22 09:50:58.640082', '2025-04-22 09:50:58.640082'),
(162, 'white deal rough timber', 'white deal rough timber - 50mm x 35mm x 4.5m ( 2 x 1 inches)', 'rough timber', '35mm x 50mm x 4.5m', '2025-04-22 09:51:03.341905', '2025-04-22 09:51:03.341904'),
(163, 'carbery 1350l non potable water tank black', 'carbery 1350l non potable water tank black', 'rough timber', '', '2025-04-22 09:51:03.341938', '2025-04-22 09:51:03.341938'),
(164, 'rough white deal batten treated', '4.8m 50 x 35 sr82 rough white deal batten treated', 'rough timber', '35mm x 50mm x 4.8m', '2025-04-22 09:51:10.597602', '2025-04-22 09:51:10.597601'),
(165, 'cls white deal', '4.8m 150 x 50 cls white deal', 'rough timber', '50mm x 150mm x 4.8m', '2025-04-22 09:51:10.597693', '2025-04-22 09:51:10.597693'),
(166, 'cls white deal', '4.8m 100 x 50 cls white deal', 'rough timber', '50mm x 100mm x 4.8m', '2025-04-22 09:51:10.597714', '2025-04-22 09:51:10.597714'),
(167, 'rough white deal treated', '4.2m 50 x 44 rough white deal treated', 'rough timber', '44mm x 50mm x 4.2m', '2025-04-22 09:51:10.597730', '2025-04-22 09:51:10.597730'),
(168, 'scaffolding plank', 'scaffolding plank 1.25m 225 x 63mm', 'rough timber', '63mm x 225mm x 1.25m', '2025-04-22 09:51:10.597747', '2025-04-22 09:51:10.597747'),
(169, 'cls white deal treated', '4.8m 150 x 50 cls white deal treated', 'rough timber', '50mm x 150mm x 4.8m', '2025-04-22 09:51:10.597764', '2025-04-22 09:51:10.597763'),
(170, 'rough white deal', '4.8m 175 x 35 rough white deal', 'rough timber', '35mm x 175mm x 4.8m', '2025-04-22 09:51:10.597800', '2025-04-22 09:51:10.597800'),
(171, 'imported rough white deal', '6.6m 225 x 44 imported rough white deal', 'rough timber', '44mm x 225mm x 6.6m', '2025-04-22 09:51:10.597817', '2025-04-22 09:51:10.597817'),
(172, 'imported rough white deal', '6.0m 225 x 44 imported rough white deal', 'rough timber', '44mm x 225mm x 6.0m', '2025-04-22 09:51:10.597833', '2025-04-22 09:51:10.597833'),
(173, 'white deal rough treated', '4.8m 75 x 35 white deal rough - treated', 'rough timber', '35mm x 75mm x 4.8m', '2025-04-22 09:51:10.597848', '2025-04-22 09:51:10.597848'),
(174, 'rough white deal treated', '4.5m 50 x 35 rough white deal treated', 'rough timber', '35mm x 50mm x 4.5m', '2025-04-22 09:51:10.597866', '2025-04-22 09:51:10.597866'),
(175, 'rough white deal treated', '5.1m 50 x 22 rough white deal treated', 'rough timber', '22mm x 50mm x 5.1m', '2025-04-22 09:51:17.387549', '2025-04-22 09:51:17.387548'),
(176, 'white deal rough treated', '5.4m 175 x 44 white deal rough - treated', 'rough timber', '44mm x 175mm x 5.4m', '2025-04-22 09:51:17.387635', '2025-04-22 09:51:17.387635'),
(177, 'rough timber', '75mm x 35mm rough timber (3 x 1.5) 2.7m (8.9\')', 'rough timber', '35mm x 75mm x 2.7m', '0001-01-01 00:00:00.000000', '2025-04-22 09:52:30.614095'),
(178, 'rough timber', '150mm x 44mm rough timber c16 en14081 (6 x 2) 6.6m (21.7\')', 'rough timber', '44mm x 150mm x 6.6m', '0001-01-01 00:00:00.000000', '2025-04-22 09:53:31.961576'),
(179, 'rough timber', '150mm x 44mm rough timber c16 en14081 (6 x 2) 7.2m (23.6\')', 'rough timber', '44mm x 150mm x 7.2m', '0001-01-01 00:00:00.000000', '2025-04-22 09:53:33.966868'),
(180, 'rough timber', '175mm x 22mm rough timber (7 x 1) 4.2m (13.8\')', 'rough timber', '22mm x 175mm x 4.2m', '0001-01-01 00:00:00.000000', '2025-04-22 09:53:54.655096'),
(181, 'rough timber', '175mm x 35mm rough timber c16 en14081 (7 x 1.5) 4.8m (15.7\')', 'rough timber', '35mm x 175mm x 4.8m', '0001-01-01 00:00:00.000000', '2025-04-22 09:53:58.110218'),
(182, 'scaffold board', 'scaffold board 2420 x 225 x 63mm (8\')', 'rough timber', '63mm x 225mm x 2420mm', '0001-01-01 00:00:00.000000', '2025-04-22 09:54:38.259779'),
(183, 'angle fillet', '4.8m 75mm x 75mm angle fillet (1 to give 2) en14081 (16\' 3 x 3)', 'rough timber', '75mm x 75mm x 4.8m', '0001-01-01 00:00:00.000000', '2025-04-22 09:54:41.743499');

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
('20250105131816_InitialCreate', '6.0.33'),
('20250412142254_AddProductUrlToPrice', '6.0.33');

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
  MODIFY `ScrapeId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `merchants`
--
ALTER TABLE `merchants`
  MODIFY `MerchantId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `prices`
--
ALTER TABLE `prices`
  MODIFY `PriceId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=259;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ProductId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=184;

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
