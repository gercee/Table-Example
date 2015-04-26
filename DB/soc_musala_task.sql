-- phpMyAdmin SQL Dump
-- version 4.0.4.2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Apr 25, 2015 at 06:41 PM
-- Server version: 5.5.28
-- PHP Version: 5.3.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `soc_musala_task`
--
CREATE DATABASE IF NOT EXISTS `soc_musala_task` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `soc_musala_task`;

-- --------------------------------------------------------

--
-- Table structure for table `mus_category`
--

CREATE TABLE IF NOT EXISTS `mus_category` (
  `categoryID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(128) CHARACTER SET cp1251 COLLATE cp1251_bulgarian_ci NOT NULL,
  `dateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `dateModified` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`categoryID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=32 ;

-- --------------------------------------------------------

--
-- Table structure for table `mus_documents`
--

CREATE TABLE IF NOT EXISTS `mus_documents` (
  `documentID` int(11) NOT NULL AUTO_INCREMENT,
  `categoryID` int(11) NOT NULL,
  `name` varchar(128) CHARACTER SET cp1251 COLLATE cp1251_bulgarian_ci NOT NULL,
  `description` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_bulgarian_ci DEFAULT NULL,
  `location` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_bulgarian_ci DEFAULT NULL,
  `dateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `dateModified` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`documentID`),
  KEY `mus_documents_FKIndex1` (`categoryID`),
  KEY `IFK_Rel_04` (`categoryID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=47 ;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `mus_documents`
--
ALTER TABLE `mus_documents`
  ADD CONSTRAINT `mus_documents_ibfk_1` FOREIGN KEY (`categoryID`) REFERENCES `mus_category` (`categoryID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
